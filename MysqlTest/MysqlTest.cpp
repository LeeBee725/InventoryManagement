#include "mysql_helper.h"
#include "mysql_auth.h"

InventoryDBHelper manager;

int main() {
    try {
        manager.insertRow("명품불고기", 3);
        manager.insertRow("명품갈릭디핑소스", 12);
        manager.printRows();

        manager.modifyRow("명품불고기", 5);
        manager.printRows();

        manager.deleteRow("명품갈릭디핑소스");
        manager.printRows();

        manager.deleteRow("명품불고기");
        manager.printRows();

    } catch (const mysqlx::Error &err) {
        std::cout << "ERROR: " << err << std::endl;
        return 1;
    } catch (std::exception &ex) {
        std::cout << "STD EXCEPTION: " << ex.what() << std::endl;
        return 1;
    } catch (const char *ex) {
        std::cout << "EXCEPTION: " << ex << std::endl;
        return 1;
    }

    return 0;
}