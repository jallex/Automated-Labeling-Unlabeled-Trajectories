import bpy, bmesh
C = bpy.context

for o in ("Track", "Camera"):
   obj = bpy.context.scene.objects.get(o)
   if obj: obj.select_set(True)
a = bpy.context.selected_objects[0]
b = bpy.context.selected_objects[1]
location1 = bpy.context.selected_objects[0].matrix_world.translation
location2 = bpy.context.selected_objects[1].matrix_world.translation

print(location1)
print(location2)
   
m = bpy.data.meshes.new('connector')

bm = bmesh.new()
v1 = bm.verts.new( location1 )
v2 = bm.verts.new( location2 )
e  = bm.edges.new([v1,v2])
bm.to_mesh(m)

o = bpy.data.objects.new( 'connector', m )
C.scene.collection.objects.link( o )

# Hook connector vertices to respective cylinders
for i, cyl in enumerate([ a, b ]):
    bpy.ops.object.select_all( action = 'DESELECT' )
    cyl.select_set(True)
    o.select_set(True)
    C.view_layer.objects.active = o # Set connector as active

    # Select vertex
    bpy.ops.object.mode_set(mode='OBJECT')
    o.data.vertices[i].select = True    
    bpy.ops.object.mode_set(mode='EDIT')

    bpy.ops.object.hook_add_selob() # Hook to cylinder

    bpy.ops.object.mode_set(mode='OBJECT')
    o.data.vertices[i].select = False 

m = o.modifiers.new('Skin', 'SKIN')

## New bit starts here
m.use_smooth_shade = True

m = o.modifiers.new('Subsurf', 'SUBSURF' )
m.levels = 2
m.render_levels = 2

## End of new bit
bpy.ops.object.select_all( action = 'DESELECT' )

obj = bpy.data.objects['connector']
for v in obj.data.skin_vertices[0].data:
    v.radius = 0.1, 0.1