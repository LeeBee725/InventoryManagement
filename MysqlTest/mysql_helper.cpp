#include "mysql_helper.h"

InventoryDBHelper::InventoryDBHelper():sess(Session(HOST, 33060, USER, PWD, DB)), curTable(sess.getDefaultSchema().getTable(TABLE)){
	std::cout << "Creating session...\n";
	// 보안을 위해 숨겨놓은 파일에서 mysql 정보를 가져온다.
	std::cout << "Complete to create session.\n";
}
InventoryDBHelper::~InventoryDBHelper(){
	sess.close();
}

void InventoryDBHelper::useTable(const string& name) {
	curTable = sess.getDefaultSchema().getTable(name);
}

void InventoryDBHelper::insertRow(const string& name, const int& num){
	curTable.insert("name", "num").values(name, num).execute();
}
void InventoryDBHelper::deleteRow(const string& name){
	curTable.remove().where("name like :name").bind("name", name).execute();	
}
void InventoryDBHelper::modifyRow(const string& name, const int& num){
	curTable.update().set("num", num).where("name like :name").bind("name", name).execute();
}
void InventoryDBHelper::printRows() {
	RowResult result = curTable.select("*").execute();
	std::vector<Row> v = result.fetchAll();
	std::cout << "---------------------------------------------------------------------------------\n";
	for (int i = 0; i < v.size(); ++i) {
		std::cout << v[i][0] << " " << v[i][1] << " " << v[i][2] << "\n";
	}
	std::cout << "---------------------------------------------------------------------------------\n";
}