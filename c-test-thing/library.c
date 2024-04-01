#include "library.h"
#include "sqlite3.h"

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

sqlite3 *db;
char *err_msg = 0;

void hello(void) {
    printf("Hello, World!\n");
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

static int callback(void *data, int argc, char **argv, char **az_col_name);

Product* find_product_by_id(char *db_path, int id) {
    int rc = init(db_path);
    if (rc) {
        return NULL;
    }

    Product *product;

    char sql[100];
    snprintf(sql, sizeof(sql), "SELECT * FROM Products WHERE Id = %d LIMIT 1", id);

    printf("The query is: %s\n", sql);

    rc = sqlite3_exec(db, sql, callback, &product, &err_msg);
    if (rc != SQLITE_OK) {
        fprintf(stderr, "SQL error: %s\n", err_msg);
        sqlite3_free(err_msg);
    }

    sqlite3_close(db);

    return product;
}

static int callback(void *data, int argc, char **argv, char **az_col_name) {
    Product **product_ptr = (Product **)data;
    *product_ptr = (Product*)malloc(sizeof(Product));

    for (int i = 0; i < argc; i++) {
        if (strcmp(az_col_name[i], "Id") == 0) {
            printf("Id is: %s\n", argv[i]);
            (*product_ptr)->id = atoi(argv[i]);
        }
        else if (strcmp(az_col_name[i], "Name") == 0) {
            printf("Name is: %s\n", argv[i]);
            (*product_ptr)->name =  strdup(argv[i]);
        }
    }

    return SQLITE_OK;
}

int init(char *db_path) {
    int rc = sqlite3_open(db_path,&db);
    if (rc != SQLITE_OK) {
        printf("Could not open database:\n%s\n", sqlite3_errmsg(db));  // or print to stderr
        return 1;
    }
    return 0;

//    printf("Address of database handle: %p\n", db);
//
//    const char *sql_select_data = "SELECT * FROM ProductCategories;";
//    rc = sqlite3_exec(db, sql_select_data, print_row, 0, &err_msg);
//    if (rc != SQLITE_OK) {
//        fprintf(stderr, "SQL error: %s\n", err_msg);
//        sqlite3_free(err_msg);
//        sqlite3_close(db);
//        return;
//    }
//
//    sqlite3_close(db);
}

void free_product(Product *product) {
    if (product != NULL) {
        free(product);
    }
}