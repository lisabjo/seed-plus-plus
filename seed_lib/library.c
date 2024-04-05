#include "library.h"
#include "sqlite3.h"

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

sqlite3 *db;
char *err_msg = 0;

static int hydrate_product(void *data, int argc, char **argv, char **az_col_name);

int print_row(void *_, int argc, char **argv, char **azColName) {
    int i;
    for(i = 0; i < argc; i++) {
        printf("%s = %s\n", azColName[i], argv[i] ? argv[i] : "NULL");
    }
    printf("-----------------------------\n");
    return 0;
}

Product* find_product_by_id(char *db_path, int id) {
    int rc = init(db_path);
    if (rc) {
        return NULL;
    }

    char sql[100];
    snprintf(sql, sizeof(sql), "SELECT * FROM Products WHERE Id = %d LIMIT 1", id);

    Product *product = 0;

    rc = sqlite3_exec(db, sql, hydrate_product, &product, &err_msg);
    if (rc != SQLITE_OK) {
        fprintf(stderr, "SQL error: %s\n", err_msg);
        sqlite3_free(err_msg);
    }

    sqlite3_close(db);
    return product;
}

static int hydrate_product(void *data, int argc, char **argv, char **az_col_name) {
    Product* product = (Product*)malloc(sizeof(Product));
    *((Product **)data) = product;

    for (int i = 0; i < argc; i++) {
        if (strcmp(az_col_name[i], "Id") == 0) {
            product->id = atoi(argv[i]);
        }
        else if (strcmp(az_col_name[i], "Name") == 0) {
            product->name = strdup(argv[i]);
        }
        else if (strcmp(az_col_name[i], "Price") == 0) {
            product->price = atof(argv[i]);
        }
        else if (strcmp(az_col_name[i], "TypeId") == 0) {
            product->type_id = atoi(argv[i]);
        }
        else if (strcmp(az_col_name[i], "CategoryId") == 0) {
            product->category_id = atoi(argv[i]);
        }
        else if (strcmp(az_col_name[i], "NumberInStock") == 0) {
            product->num_in_stock = atoi(argv[i]);
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
}

void free_product(Product *product) {
    if (product != NULL) {
        free(product);
    }
}
