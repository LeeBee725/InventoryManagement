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

    inline Session& getSession() { return sess; }

    void useTable(const string& name);

    void insertRow(const string& name, const int& num);
    void deleteRow(const string& name);
    void modifyRow(const string& name, const int& num);
    void printRows();
};