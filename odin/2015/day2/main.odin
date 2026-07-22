package main

import "core:strconv"
import "core:fmt"
import "core:os"
import "core:strings"

main :: proc() {
    data, err := os.read_entire_file("input", context.allocator)
    defer delete(data, context.allocator)

    areas: [3]int
    total := 0
    it := string(data)

    for line in strings.split_lines_iterator(&it) {
        dimensions := strings.split(line, "x")
        l, _ := strconv.parse_int(dimensions[0])
        w, _ := strconv.parse_int(dimensions[1])
        h, _ := strconv.parse_int(dimensions[2])
        delete(dimensions)

        areas[0] = l * w
        areas[1] = l * h
        areas[2] = w * h

        total += (
            min(areas[0], areas[1], areas[2]) + 
            (2 * (areas[0] + areas[1] + areas[2]))
        )
    }
    fmt.println(total)
}