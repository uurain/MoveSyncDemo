#!/usr/bin/env python
#coding=utf-8

from xls2lua import *
from FileUtil import *

import os
import sys

reload(sys)
sys.setdefaultencoding('utf8')

targetPath = r"./lua/"
sourcePath = r"./excel/"

def CreateRequireLua():
	outfp = open(targetPath + "DataTable.lua", 'w')
	create_time = time.strftime("%a %b %d %H:%M:%S %Y", time.gmtime(time.time()))
	outfp.write("-- create time:" + create_time)
	outfp.write("\n")
	outfp.write('\n\npreLoadData = \n{\n')
	for dirpath,dirs,files in os.walk(targetPath):
		for file in files:
			if file != "DataTable.lua":
				outfp.write("	\"Table/" + os.path.splitext(file)[0] + "\",\n")
	outfp.write("}")
	outfp.close()

def CreateAll():
	for dirpath,dirs,files in os.walk(sourcePath):
		for file in files:
			if os.path.splitext(file)[1] == ".xls":
				srcFile = os.path.join(os.getcwd() + r'/excel', file)
				translate(srcFile, targetPath, os.path.splitext(file)[0])
	CreateRequireLua()

def MoveLuaToProject():
	FileUtil.copyTree(os.getcwd() + targetPath, u'D:/NAME_TEMP/src/client/dt/Assets/LuaFramework/Lua/Table/')


CreateAll()
MoveLuaToProject()