#pragma once

#include <mysqlx/xdevapi.h>
#include <iostream>
#include <vector>
#include "mysql_auth.h"

using namespace mysqlx;

class InventoryDBHelper {
private:
    Session sess;
    Table curTable;

public:
    InventoryDBHelper();
    ~InventoryDBHelper();

    void createTable(const string& name);
    void useTable(const string& name);

    void insertItem(const string& name, const int& num, const string& containerName);
    void deleteItem(const string& name);
    void modifyItem(const string& name, const int& num, const string& containerName);
    void modifyItemNum(const string& name, const int& num);
    void modifyItemContainer(const string& name, const string& containerName);
    std::vector<Row> fetchAllItems();
    void printItems();
};