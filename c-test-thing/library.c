#include "library.h"
#include "sqlite3.h"

#include <stdio.h>

sqlite3 *db;
char *err_msg = 0;

void hello(void) {
    printf("Hello, World!\n");
}

void foo(void) {
    printf("Hello, Foooooo!\n");
}

int print_row(void *_, int argc, char **argv, char **azColName) {
    int i;
    for(i = 0; i < argc; i++) {
        printf("%s = %s\n", azColName[i], argv[i] ? argv[i] : "NULL");
    }
    printf("-----------------------------\n");
    return 0;
}

void insert_it() {

}

void init(char *db_path) {
    int rc = sqlite3_open(db_path,&db);
    if (rc) {
        printf("Could not open database.\n");
        return;
    }

    printf("Address of database handle: %p\n", db);

    const char *sql_select_data = "SELECT * FROM ProductCategories;";
    rc = sqlite3_exec(db, sql_select_data, print_row, 0, &err_msg);
    if (rc != SQLITE_OK) {
        printf("aer du konstig\n");
        fprintf(stderr, "SQL error: %s\n", err_msg);
        sqlite3_free(err_msg);
        sqlite3_close(db);
        return;
    }

    sqlite3_close(db);
}