#ifndef C_TEST_THING_LIBRARY_H
#define C_TEST_THING_LIBRARY_H

typedef struct {
    int id;
    char *name;
} Product;

void hello(void);
void foo(void);
int init(char *db_path);
Product* find_product_by_id(char *db_path, int id);
void free_product(Product *product);

#endif //C_TEST_THING_LIBRARY_H
