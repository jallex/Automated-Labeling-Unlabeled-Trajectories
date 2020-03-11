import bpy
import csv

#script to read csv files in blender 
#adjust file location
with open(r"C:\Users\jacki\OneDrive\Desktop\blender-scripts\Juggling0001.csv", "r") as csv_file:
    file = list(csv.reader(csv_file))
    #the data from frame 1
    frame = 0
    currentRow = file[frame + 11]
    cols, rows = (3, int((len(currentRow) - 2) / 3))
    arr = a = [[None]*cols for _ in range(rows)]
    count = 0
    countTotal = 0
    countRow = 0
    #print(currentRow)
    for x in range(2, len(currentRow)):
        arr[countRow][count] = currentRow[x]
        #print(arr[countRow][count])
        count += 1
        if (count == 3):
            count = 0
            countRow += 1
        if x == len(currentRow) - 10:
            print(arr)
