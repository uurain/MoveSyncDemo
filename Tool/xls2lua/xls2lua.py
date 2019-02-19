#! /usr/bin/env python
# -*- coding: utf-8 -*
# author: luzexi

import xlrd
import os.path
import time
import os

SCRIPT_HEAD = "-- source file: %s\n\
-- created at: %s\n\
\n\
\n\
"

def make_table(filename):
	if not os.path.isfile(filename):
		raise NameError, "%s is	not	a valid	filename" % filename
	# book_xlrd = xlrd.open_workbook(filename,formatting_info=True)
	book_xlrd = xlrd.open_workbook(filename)

	excel = {}
	excel = {}
	excel["filename"] = filename
	excel["data"] = {}
	excel["meta"] = {}
	for sheet in book_xlrd.sheets():
		sheet_name = sheet.name.replace(" ", "_")
		if sheet_name.startswith("test_"):
			continue
		print(sheet_name+" sheet")
		excel["data"][sheet_name] = {}
		excel["meta"][sheet_name] = {}

		# 必须大于2行
		if sheet.nrows <= 3:
			return {}, -1, "sheet[" + sheet_name + "]" + " rows must > 3"

		# 解析标题
		title = {}
		col_idx = 0
		for col_idx in xrange(sheet.ncols):
			value = sheet.cell_value(0, col_idx)
			vtype = sheet.cell_type(0, col_idx)
			if vtype != 1:
				return {}, -1, "title columns[" + str(col_idx) + "] must be string"
			title[col_idx] = str(value).replace(" ", "_")
 
		excel["meta"][sheet_name]["title"] = title

		# 类型解析
		type_dict = {}
		col_idx = 0
		for col_idx in xrange(sheet.ncols):
			value = sheet.cell_value(2, col_idx)
			vtype = sheet.cell_type(2, col_idx)
			type_dict[col_idx] = str(value)
			if (type_dict[col_idx].lower() != "int" \
				and type_dict[col_idx].lower() != "float" \
				and type_dict[col_idx].lower() != "string" \
				and type_dict[col_idx].lower() != "bool"\
				and type_dict[col_idx].lower() != "byte"\
				and type_dict[col_idx].lower() != "ai"\
				and type_dict[col_idx].lower() != "af"\
				and type_dict[col_idx].lower() != "as"\
				and type_dict[col_idx].lower() != "ab"):
				return {}, -1, "sheet[" + sheet_name + "]" + \
					" row[" + row_idx + "] column[" + col_idx + \
					"] type must be [i] or [s] or [b] or [ai] or [as] or [ab]"

		if type_dict[0].lower() != "int":
			return {}, -1,"sheet[" + sheet_name + "]" + " first column type must be [i]"

		excel["meta"][sheet_name]["type"] = type_dict
		id_dict = {}

		row_idx = 3
		# 数据从第4行开始
		for row_idx in xrange(3, sheet.nrows):
			row = {}

			col_idx = 0
			for col_idx in xrange(sheet.ncols):
				value = sheet.cell_value(row_idx, col_idx)
				vtype = sheet.cell_type(row_idx, col_idx)
				# 本行有数据
				v = None
				if type_dict[col_idx].lower() == "int" and vtype == 2:
					v = int(value)
				if type_dict[col_idx].lower() == "byte" and vtype == 2:
					v = int(value)
				elif type_dict[col_idx].lower() == "float" and vtype == 2:
					v = float(value)
				elif type_dict[col_idx].lower() == "string":
					v = format_str(value)
				elif type_dict[col_idx].lower() == "bool" and vtype == 4:
					if value == 1:
						v = "true"
					else:
						v = "false"
				elif type_dict[col_idx].lower() == "ai" and vtype == 1:
					v = str(value)
				elif type_dict[col_idx].lower() == "af" and vtype == 1:
					v = str(value)
				elif type_dict[col_idx].lower() == "as":
					v = format_str(value)
				elif type_dict[col_idx].lower() == "ab" and vtype == 1:
					v = str(value)

				row[col_idx] = v
				if col_idx == 0:
					id_dict[row_idx - 3] = v

			excel["data"][sheet_name][row[0]] = row
		excel["meta"][sheet_name]["id_dict"] = id_dict

	return excel, 0 , "ok"

def format_str(v):
	# print(""+v)
	# s = (""+v).encode("utf-8")
	# print(s)
	# s = "" + v
	# bytes(num)
	if type(v) == int or type(v) == float :
		v =  bytes(v)
	s = ("%s"%(""+v)).encode("utf-8")
	s = s.replace('\"','\\\"')
	s = s.replace('\'','\\\'')
	# if s[-1] == "]":
	# 	s = "%s "%(s)
	return s

def get_i(v):
	if v is None:
		return 0
	return v

def get_f(v):
	if v is None:
		return 0
	return v

def get_s(v):
	if v is None:
		return ""
	return v

def get_b(v):
	if v is None:
		return "false"
	return v

def get_ai( v ):
	if v is None:
		return "{}"
	tmp_vec_str = v.split(';')
	res_str = "{"
	i = 0
	for val in tmp_vec_str:
		if val <> None and val <> "":
			if i <> 0:
				res_str += ","
			res_str = res_str + val
			i+=1
	res_str += "}"
	return res_str

def get_af( v ):
	if v is None:
		return "{}"
	tmp_vec_str = v.split(';')
	res_str = "{"
	i = 0
	for val in tmp_vec_str:
		if val <> None and val <> "":
			if i <> 0:
				res_str += ","
			res_str = res_str + val
			i+=1
	res_str += "}"
	return res_str

def get_as( v ):
	if v is None:
		return "{}"
	tmp_vec_str = v.split(';')
	res_str = "{"
	i = 0
	for val in tmp_vec_str:
		if val <> None and val <> "":
			if i <> 0:
				res_str += ","
			res_str = res_str + "\"" + val + "\""
			i+=1
	res_str += "}"
	return res_str

def get_ab( v ):
	if v is None:
		return "{}"
	tmp_vec_str = v.split(';')
	res_str = "{"
	i = 0
	for val in tmp_vec_str:
		if val <> None and val <> "":
			if i <> 0:
				res_str += ","
			res_str = res_str + val.lower()
			i+=1
	res_str += "}"
	return res_str

def write_to_lua_script(excel, output_path, luaFileName):
	if not os.path.exists(output_path):
		os.mkdir(output_path)

	outfp = open(output_path + "/" + luaFileName + ".lua", 'w')
	create_time = time.strftime("%a %b %d %H:%M:%S %Y", time.gmtime(time.time()))
	outfp.write(SCRIPT_HEAD % (excel["filename"], create_time)) 
	tableName = luaFileName + "Table"
	outfp.write(tableName + " = {\n")

	for (sheet_name, sheet) in excel["data"].items():
		# outfp = open(output_path + "/" + sheet_name + ".lua", 'w')
		# outfp.write("\n")

		outfp.write("	" + sheet_name + " = {\n")
		print("itemcount")
		title = excel["meta"][sheet_name]["title"]
		type_dict= excel["meta"][sheet_name]["type"]
		id_dict = excel["meta"][sheet_name]["id_dict"]
		
		row_index = 1
		for (row_idx, row) in sheet.items():
			outfp.write("		[" + str(id_dict[row_index - 1]) + "] = {")
			row_index += 1
			field_index = 0
			for (col_idx, field)in row.items():
				if field_index > 0:
						outfp.write(",")
				field_index += 1
				if type_dict[col_idx] == "int":
					tmp_str = get_i(row[col_idx])
					outfp.write(" " + str(title[col_idx]) + " = " + str(tmp_str))
				elif type_dict[col_idx] == "byte":
					tmp_str = get_i(row[col_idx])
					outfp.write(" " + str(title[col_idx]) + " = " + str(tmp_str))
				elif type_dict[col_idx] == "float":
					tmp_str = get_f(row[col_idx])
					outfp.write(" " + str(title[col_idx]) + " = " + str(tmp_str))
				elif type_dict[col_idx] == "string":
					tmp_str = get_s(row[col_idx])
					outfp.write(" " + str(title[col_idx]) + " = \"" + str(tmp_str) + "\"")
				elif type_dict[col_idx] == "bool":
					tmp_str = get_b(row[col_idx])
					outfp.write(" " + str(title[col_idx]) + " = " + str(tmp_str))
				elif type_dict[col_idx] == "ai":
					tmp_str = get_ai(row[col_idx])
					outfp.write(" " + str(title[col_idx]) + " = " + str(tmp_str))
				elif type_dict[col_idx] == "af":
					tmp_str = get_af(row[col_idx])
					outfp.write(" " + str(title[col_idx]) + " = " + str(tmp_str))
				elif type_dict[col_idx] == "as":
					tmp_str = get_as(row[col_idx])
					outfp.write(" " + str(title[col_idx]) + " = " + str(tmp_str))
				elif type_dict[col_idx] == "ab":
					tmp_str = get_ab(row[col_idx])
					outfp.write(" " + str(title[col_idx]) + " = " + str(tmp_str))
				else:
					outfp.close()
					sys.exit("error: there is some wrong in type.")

			outfp.write("},\n")
		outfp.write('	},\n')
	outfp.write('}')
	outfp.close()

def translate(filePath, output_path, fileName):
	if not os.path.exists(filePath):
		sys.exit("error: "+filePath+" is not exists.")
	print("begin translate:" + filePath)
	t, ret, errstr = make_table(filePath)
	if ret != 0:
		print(filePath)
		print "error: " + errstr
	else:
		print(filePath)
		print "success!!!"
	print(fileName)
	write_to_lua_script(t, output_path, fileName)

# def main():
# 	import sys
# 	if len(sys.argv) < 3:
# 		sys.exit('''usage: xls2lua.py excel_name output_path''')
# 	filename = sys.argv[1]
# 	output_path = sys.argv[2]
# 	if not os.path.exists(filename):
# 		sys.exit("error: "+filename+" is not exists.")
# 	t, ret, errstr = make_table(filename)
# 	if ret != 0:
# 		print(filename)
# 		print "error: " + errstr
# 	else:
# 		print(filename)
# 		print "res:"
# 		# print(t)
# 		print "success!!!"
# 	write_to_lua_script(t, output_path)

# if __name__=="__main__":
# 	main()

