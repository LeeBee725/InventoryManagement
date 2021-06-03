#include "mysql_helper.h"

InventoryDBHelper::InventoryDBHelper() :sess(Session(HOST, 33060, USER, PWD, DB)), curTable(sess.getDefaultSchema().getTable(TABLE)) {
	std::cout << "Creating session... " << HOST << ", " << USER << " " << DB << "\n";
	// 보안을 위해 숨겨놓은 파일에서 mysql 정보를 가져온다.
	std::cout << "Complete to create session.\n";
}
InventoryDBHelper::~InventoryDBHelper() {
	sess.close();
}

void InventoryDBHelper::createTable(const string& name) {
	sess.getDefaultSchema().createCollection(name);
}

void InventoryDBHelper::useTable(const string& name) {
	curTable = sess.getDefaultSchema().getTable(name);
}

void InventoryDBHelper::insertItem(const string& name, const int& num, const string& containerName) {
	curTable.insert("name", "num", "container").values(name, num, containerName).execute();
}
void InventoryDBHelper::deleteItem(const string& name) {
	curTable.remove().where("name like :name").bind("name", name).execute();
}
void InventoryDBHelper::modifyItem(const string& name, const int& num, const string& containerName) {
	curTable.update().set("num", num).set("container", containerName).where("name like :name").bind("name", name).execute();
}
void InventoryDBHelper::modifyItemNum(const string& name, const int& num) {
	curTable.update().set("num", num).where("name like :name").bind("name", name).execute();
}
void InventoryDBHelper::modifyItemContainer(const string& name, const string& containerName) {
	curTable.update().set("container", containerName).where("name like :name").bind("name", name).execute();
}
std::vector<Row> InventoryDBHelper::fetchAllItems() {
	RowResult results = curTable.select("*").execute();
	std::vector<Row> resultVec = results.fetchAll();

	return resultVec;
}
void InventoryDBHelper::printItems() {
	RowResult result = curTable.select("*").execute();
	std::vector<Row> v = result.fetchAll();
	std::cout << "---------------------------------------------------------------------------------\n";
	for (int i = 0; i < v.size(); ++i) {
		std::cout << v[i][0] << " " << v[i][1] << " " << v[i][2] << " " << v[i][3] << "\n";
	}
	std::cout << "---------------------------------------------------------------------------------\n";
}