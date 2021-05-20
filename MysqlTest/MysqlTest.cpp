#include <iostream>
#include "mysql_helper.h"
#include "mysql_auth.h"

int main() {
     try {

         std::cout << "Creating session...\n";

         // 보안을 위해 숨겨놓은 파일에서 mysql 정보를 가져온다.
         mysqlx::Session sess(HOST, 33060, USER, PWD, DB);
         std::cout << "Complete to create session.\n";
         // Drop the collection
         
         mysqlx::Schema db = sess.getSchema("pizza");

         // Create a new collection 'my_collection'
         mysqlx::Collection myColl = db.createCollection("my_collection");

         // Insert documents
         myColl.add(R"({"name": "Laurie", "age": 19})").execute();
         myColl.add(R"({"name": "Nadya", "age": 54})").execute();
         myColl.add(R"({"name": "Lukas", "age": 32})").execute();

         // Find a document
         mysqlx::DocResult docs = myColl.find("name like :param1 AND age < :param2").limit(1)
             .bind("param1", "L%").bind("param2", 20).execute();

         // Print document
         std::cout << docs.fetchOne();

         // Drop the collection
         db.dropCollection("my_collection");


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