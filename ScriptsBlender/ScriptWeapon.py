import sys, bpy
from math import radians
from mathutils import Matrix


def run():
    file1 = 'C:\\Users\\user\\Documents\\ColorBlood\\ColorBlood\\Assets\\PolygonDungeon\\Models\\SM_Wep_Goblin_Mace_01.fbx'
    file2 = 'C:\\Users\\user\\Desktop\\SM_Wep_Goblin_Mace.fbx'
    rotateWeapon(file1, file2)
    
def removeAllObj():
    # print all objects
    objs = bpy.data.objects
    for obj in bpy.data.objects:
        objs.remove(obj)

def importFile(path):
    bpy.ops.import_scene.fbx(filepath = path)

def exportFile(path):
    bpy.ops.export_scene.fbx(filepath = path)

def rotateObj(obj, degree, axis):
    rot = Matrix.Rotation(radians(degree), 4, axis)
    obj.matrix_world *= rot
    obj.rotation_euler = obj.matrix_world.to_euler()

def scaleObj(obj, sizeX, sizeY, sizeZ):
    obj.scale = (sizeX, sizeY, sizeZ)

def rotateWeapon(pathImport, pathExport):
    removeAllObj()
    importFile(pathImport)
    weapon = bpy.data.objects[0]
    rotateObj(weapon, 90, 'X')
    rotateObj(weapon, -90, 'Y')
    scaleObj(weapon, 1, 1, 1)
    exportFile(pathExport)