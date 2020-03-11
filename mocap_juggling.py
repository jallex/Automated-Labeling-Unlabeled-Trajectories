
import bpy

#iterate through each object in this blender project
for tracker in bpy.data.objects:
    #if the object is an empty
    if tracker.type == 'EMPTY':
        #Add a bone
        bpy.ops.object.armature_add(enter_editmode=False, location=tracker.matrix_world.translation)
        #resize the bone
        bpy.ops.transform.resize(value=(0.5, 0.5, 0.5), orient_type='GLOBAL', orient_matrix=((1, 0, 0), (0, 1, 0), (0, 0, 1)), orient_matrix_type='GLOBAL', mirror=True, use_proportional_edit=False, proportional_edit_falloff='SMOOTH', proportional_size=1, use_proportional_connected=False, use_proportional_projected=False, release_confirm=True)
        new_armature = bpy.context.selected_objects[0]
        #set the bone's parent to be the empty
        new_armature.parent = tracker
        #ensure that the bone is at the same location as the tracker
        new_armature.matrix_world.translation = tracker.matrix_world.translation
        #create a cube
        bpy.ops.mesh.primitive_cube_add(size=0.1, enter_editmode=False, location=(0, 0, 0))
        new_cube = bpy.context.selected_objects[0]
        #set the cube's parent to be the bone
        new_cube.parent = new_armature
        #ensure the cube is also at the tracker's location
        new_cube.matrix_world.translation = tracker.matrix_world.translation

        