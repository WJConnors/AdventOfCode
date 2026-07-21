package main

import "core:fmt"
import "core:os"
import "core:strings"

main :: proc() {
    data, err := os.read_entire_file("input", context.allocator)
    if err != nil {
        return
    }
    defer delete(data)

    //floor := strings.count(text, "(") - strings.count(text, ")")

    floor := 0
    for r, i in data {
        switch r {
            case '(':
                floor += 1
            case ')':
                floor -= 1
        }
        if floor < 0 {
            fmt.println(i + 1)
            break
        }
    }

    fmt.println(floor)
}