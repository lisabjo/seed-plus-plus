cmake_minimum_required(VERSION 3.29)
project(c_test_thing C)

set(CMAKE_C_STANDARD 17)

# Find SQLite3 package
find_package(SQLite3 REQUIRED)

# Include SQLite3 headers
include_directories(${SQLite3_INCLUDE_DIRS})

add_library(c_test_thing SHARED library.c)

# Link against SQLite3 library
target_link_libraries(c_test_thing ${SQLite3_LIBRARIES})
