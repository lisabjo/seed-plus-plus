#pragma once

typedef struct {
    int id;
    int type_id;
    int category_id;
    int num_in_stock;
    char *name;
    double price;
} Product;

int init(char *db_path);
Product* find_product_by_id(char *db_path, int id);
void free_product(Product *product);
