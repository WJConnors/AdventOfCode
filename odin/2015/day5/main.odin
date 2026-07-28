package main

import "core:fmt"
import "core:os"
import "core:strings"

main :: proc() {
    data, _ := os.read_entire_file("input", context.allocator)
    defer delete(data, context.allocator)

    nice_count := 0
    it := string(data)
    for line in strings.split_lines_iterator(&it) {
        first_seen: [26 * 26]int
        has_double_pair := false
        has_spaced_repeat := false
        for idx in 1..<len(line) {
            c := line[idx]

            if !has_double_pair {
                prev1 := line[idx-1]
                b26_encoded := int(prev1 - 'a') * 26 + int(c - 'a')
                seen := first_seen[b26_encoded]
                if seen == 0 do first_seen[b26_encoded] = idx
                else if seen <= idx-2 do has_double_pair = true
            }

            if idx >= 2 && !has_spaced_repeat {
                has_spaced_repeat = c == line[idx - 2]
            }
            if has_double_pair && has_spaced_repeat {
                nice_count += 1
                break
            }
        }
    }
    /*for line in strings.split_lines_iterator(&it) {
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
    }*/
    fmt.println(nice_count)
}