cmake_minimum_required(VERSION 3.29)
project(seed_lib C)

set(CMAKE_C_STANDARD 17)

# Find SQLite3 package
find_package(SQLite3 REQUIRED)

# Include SQLite3 headers
include_directories(${SQLite3_INCLUDE_DIRS})

add_library(seed_lib SHARED library.c)

# Link against SQLite3 library
target_link_libraries(seed_lib ${SQLite3_LIBRARIES})
