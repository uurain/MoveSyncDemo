#!/usr/bin/python
# -*- coding: utf-8 -*-
# function: copy or remove directory tree or single file
# modify: add multiple directory travels functions
# time: 2014-11-17 16:26
# by: Nr.Sila
'''
Methods:

def rmAndMkdirs(path):
delete dirs(can contain files) and mkdirs

def copyTree(srcAbsPath, destAbsPath):

def removeTree(delAbsPath):
remove files under the directory and the directory self,read only file can be deleted, eg: SVN directory operation

def copySingleFile(srcAbsPath, destAbsPath):

def removeSingleFile(delAbsPath)


Directories' path operation
--------------------------------------------------------------------------------
def getCurrentDirSubDirsName(dirAbsPath):
get current directory's sub directories name list

def getSubDirsFullPath(dirAbsPath):
get sub directories full path name list

def getCurrentDirFilesName(dirAbsPath, filterExtList = None):
get current dir files name list

def getCurrentDirFilesFullPath(dirAbsPath, filterExtList = None):
get current directory's files full path

def getCurrentAndSubDirsFullPath(dirAbsPath, filterExtList = None):
get current and sub directories files full path via filter file extentions list

def getCurrentAndSubDirsTruncatedPath(dirAbsPath, filterExtList = None):
get current and sub directories files truncate path eg: a.png subdir/b.png

tip:
all return list[unicode]
--------------------------------------------------------------------------------'''

import sys
reload(sys)
sys.setdefaultencoding('utf-8')

import os
import os.path
import stat
import shutil
from distutils.dir_util import copy_tree
from distutils.dir_util import remove_tree
from distutils.file_util import copy_file
import fnmatch


class FileUtil(object):

    @staticmethod
    def u(str):
        '''return unicode string'''
        if isinstance(str, unicode):
            return str
        else:
            try:
                return unicode(str, sys.getfilesystemencoding())
            except Exception, e:
                print("FileUtil UnicodeDecodeError!!!")
                raise e

    @staticmethod
    def unicodeStringList(l):
        '''unicode list[str] -> list[unicode]'''
        tempList = []
        for v in l:
            tempList.append(FileUtil.u(v))
        return list(tempList)

    @staticmethod
    def checkDirPathExist(path):
        if not os.path.exists(path):
            print("Directory is not exist. --- " + path)
            sys.exit()

    @staticmethod
    def rmAndMkdirs(path):
        '''delete dirs(can contain files) and mkdirs'''
        if os.path.exists(path):
            FileUtil.removeTree(path)
        os.makedirs(path)

    @staticmethod
    def copyTree(srcAbsPath, destAbsPath):
        if not os.path.isabs(srcAbsPath) or not os.path.isabs(destAbsPath):
            print("src/dest path must be absolute path!")
            return
        if not os.path.exists(srcAbsPath):
            print("src path is not exist!")
            return
        copy_tree(srcAbsPath, destAbsPath)

    @staticmethod
    def removeTree(delAbsPath):
        '''
        remove files under the directory and the directory self
        read only file can be deleted, eg: SVN directory operation
        '''
        if not os.path.isabs(delAbsPath):
            print("del path must be absolute path!")
            return
        if not os.path.exists(delAbsPath):
            print("del path is not exist!")
            return

        def on_rm_error(func, path, exc_info):
            # path contains the path of the file that couldn't be removed
            # let's just assume that it's read-only and unlink it.
            os.chmod(path, stat.S_IWRITE)
            os.unlink(path)
        shutil.rmtree(delAbsPath, onerror=on_rm_error)
        # remove_tree(delAbsPath) can not del read only file

    @staticmethod
    def copySingleFile(srcAbsPath, destAbsPath):
        if not os.path.isabs(srcAbsPath) or not os.path.isabs(destAbsPath):
            print("src/dest path must be absolute file path!")
            return
        if not os.path.exists(srcAbsPath):
            print("src file path is not exist!")
            return
        if not os.path.isfile(srcAbsPath):
            print("src file is not file!")
            return
        shutil.copy(srcAbsPath, destAbsPath)

    @staticmethod
    def removeSingleFile(delAbsPath):
        if not os.path.isabs(delAbsPath):
            print("del file path must be absolute path!")
            return
        if not os.path.isfile(delAbsPath):
            print("del file is not file!")
            return
        os.chmod(delAbsPath, stat.S_IWRITE)
        os.remove(delAbsPath)

    @staticmethod
    def getCurrentDirSubDirsName(dirAbsPath):
        '''get current directory's sub directories name list'''
        FileUtil.checkDirPathExist(dirAbsPath)
        filesAndDirs = os.listdir(dirAbsPath)
        dirsList = []
        [dirsList.append(item) for item in filesAndDirs if os.path.isdir(
            os.path.join(dirAbsPath, item))]
        dirsList = FileUtil.unicodeStringList(dirsList)
        return list(dirsList)

    @staticmethod
    def getSubDirsFullPath(dirAbsPath):
        '''get sub directories full path name list'''
        FileUtil.checkDirPathExist(dirAbsPath)
        subDirsName = FileUtil.getCurrentDirSubDirsName(dirAbsPath)
        subDirsFullPath = []
        [subDirsFullPath.append(os.path.join(dirAbsPath, item))
         for item in subDirsName]
        subDirsFullPath = FileUtil.unicodeStringList(subDirsFullPath)
        return list(subDirsFullPath)

    @staticmethod
    def getCurrentDirFilesName(dirAbsPath, filterExtList=None):
        '''get current dir files name list'''
        FileUtil.checkDirPathExist(dirAbsPath)
        if filterExtList is None:
            filterExtList = [".*"]
        filesNameList = []
        if not isinstance(filterExtList, list):
            print(r'File extentions list eg: [".txt", ".py"]')
            return list(filesNameList)
        for v in filterExtList:
            if not v.startswith("."):
                print(r'File extentions list eg: [".txt", ".py"]')
                return list(filesNameList)

        for fileName in os.listdir(dirAbsPath):
            if os.path.isfile(os.path.join(dirAbsPath, fileName)):
                for v in filterExtList:
                    if fnmatch.fnmatch(fileName, '*' + v):
                        filesNameList.append(fileName)
        filesNameList = FileUtil.unicodeStringList(filesNameList)
        return list(filesNameList)

    @staticmethod
    def getCurrentDirFilesFullPath(dirAbsPath, filterExtList=None):
        '''get current directory's files full path'''
        FileUtil.checkDirPathExist(dirAbsPath)
        if filterExtList is None:
            filterExtList = [".*"]
        filesFullPath = []
        # map(function, list)
        # call function(list_value) in each list_value from list
        map(lambda v: filesFullPath.append(os.path.join(dirAbsPath, v)),
            FileUtil.getCurrentDirFilesName(dirAbsPath, filterExtList))
        # [filesFullPath.append(os.path.join(dirAbsPath, v)) for v in FileUtil.getCurrentDirFilesName(dirAbsPath, filterExtList)]
        filesFullPath = FileUtil.unicodeStringList(filesFullPath)
        return filesFullPath

    @staticmethod
    def _getCurrentAndSubDirsFullPath(dirAbsPath, filterExtList=None):
        '''get current and sub directories files full path via filter file extentions list'''
        FileUtil.checkDirPathExist(dirAbsPath)
        if filterExtList is None:
            filterExtList = [".*"]
        allFilesPath = []

        if not isinstance(filterExtList, list):
            print(r'File extentions list eg: [".txt", ".py"]')
            return list(allFilesPath)

        for v in filterExtList:
            if not v.startswith("."):
                print(r'File extentions list eg: [".txt", ".py"]')
                return list(allFilesPath)

        for fn in os.listdir(dirAbsPath):
            fullPath = os.path.join(dirAbsPath, fn)
            if not os.path.isdir(fullPath):
                def f(v):
                    if fnmatch.fnmatch(fullPath, '*' + v):
                        allFilesPath.append(fullPath)
                map(f, filterExtList)
            else:
                next_level_files = FileUtil._getCurrentAndSubDirsFullPath(
                    fullPath, filterExtList)
                # list.extend(seq) append a sequence(such as list..)
                allFilesPath.extend(next_level_files)
        return allFilesPath

    @staticmethod
    def getCurrentAndSubDirsFullPath(dirAbsPath, filterExtList=None):
        FileUtil.checkDirPathExist(dirAbsPath)
        if filterExtList is None:
            filterExtList = [".*"]
        curAndSubDirsFullPath = FileUtil._getCurrentAndSubDirsFullPath(
            dirAbsPath, filterExtList)
        curAndSubDirsFullPath = FileUtil.unicodeStringList(
            curAndSubDirsFullPath)
        return list(curAndSubDirsFullPath)

    @staticmethod
    def getCurrentAndSubDirsTruncatedPath(dirAbsPath, filterExtList=None):
        '''get current and sub directories files truncate path eg: a.png subdir/b.png'''
        FileUtil.checkDirPathExist(dirAbsPath)
        if filterExtList is None:
            filterExtList = [".*"]
        allFullFilesPath = FileUtil.getCurrentAndSubDirsFullPath(
            dirAbsPath, filterExtList)
        truncFilePath = []
        [truncFilePath.append(item[len(dirAbsPath) + len(os.sep):])
         for item in allFullFilesPath]
        truncFilePath = FileUtil.unicodeStringList(truncFilePath)
        return list(truncFilePath)

if __name__ == '__main__':
    # FileUtil.copyTree('/Users/nrsila/Desktop/tw', '/Users/nrsila/Desktop/tw1')
    # FileUtil.removeTree('/Users/nrsila/Desktop/tw')
    # FileUtil.removeTree('/Users/nrsila/Desktop/tw1')
    # FileUtil.copySingleFile('C:/Python27/README.txt', 'C:/Python27/README1.txt')
    # FileUtil.removeSingleFile('C:/Python27/README1.txt')

    # if filterExtList is None that is means all file types included
    # fileNameList = FileUtil.getCurrentDirFilesName(r'E:\koudaiwow\soldier', [".plist", ".py"])
    # print(fileNameList)

    # filesFullPath = FileUtil.getCurrentDirFilesName(ur'C:\Download')
    # for v in filesFullPath:
    # 	print v.encode("gb2312")
    import platform
    if platform.system() == "Darwin":
        allPath = FileUtil.getCurrentAndSubDirsFullPath(ur'/Users/nrsila/Downloads', [".pdf"])
        for v in allPath:
            print(v.encode("utf-8"))
    elif platform.system() == "Windows":
        allPath = FileUtil.getCurrentAndSubDirsFullPath(ur'C:/Download', [".pdf", ".exe"])
        for v in allPath:
            print(v.encode("utf-8"))
            # print(v.encode(sys.getfilesystemencoding())) #under cmd console

    # print(FileUtil.getCurrentDirFilesName.__doc__)
