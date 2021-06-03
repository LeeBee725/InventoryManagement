#include "mysql_helper.h"
#include "mysql_auth.h"

InventoryDBHelper manager;

int main() {
    try {
        manager.insertItem("명품불고기", 3, "토핑냉장고");
        manager.insertItem("명품갈릭디핑소스", 12, "큰냉장고");
        //manager.insertItem("피클", 100, "음료냉장고");
        manager.printItems();

        manager.modifyItemNum("명품불고기", 5);
        manager.printItems();

        manager.deleteItem("명품갈릭디핑소스");
        manager.printItems();

        manager.deleteItem("명품불고기");
        manager.printItems();

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