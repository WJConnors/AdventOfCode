package main

import "core:fmt"
import "core:os"
import "core:strings"

main :: proc() {
    data, _ := os.read_entire_file("input", context.allocator)
    defer delete(data, context.allocator)

    niceCount := 0
    it := string(data)
    for line in strings.split_lines_iterator(&it) {
        vowel_count := 0
        has_double := false
        has_forbidden := false
        for idx in 0..<len(line) {
            c := line[idx]

            switch c {
                case 'a','e','i','o','u':
                    vowel_count += 1
            }

            if idx == 0 { continue }
            prev := line[idx-1]

            if (
                (prev == 'a' && c == 'b') ||
                (prev == 'c' && c == 'd') ||
                (prev == 'p' && c == 'q') ||
                (prev == 'x' && c == 'y')
            ) {
                has_forbidden = true
                break
            }

            if !has_double && prev == c { has_double = true }
        }
        if vowel_count >= 3 && has_double && !has_forbidden { niceCount += 1 }
    }
    fmt.println(niceCount)
}