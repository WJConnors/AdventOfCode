package main

import "core:fmt"
import "core:os"
import "core:strings"
import "core:strconv"
import "core:slice"
import "core:math"

Command :: enum{toggle, on, off}
Point :: struct{x, y: int}

TOGGLE_PREFIX :: "toggle "
ON_PREFIX :: "turn on "
OFF_PREFIX :: "turn off "
THROUGH :: " through "

word_to_point :: proc(coord: string) -> Point {
    x_text, _, y_text := strings.partition(coord, ",")
    x, _:= strconv.parse_int(x_text)
    y, _:= strconv.parse_int(y_text)
    return Point{x, y}
}

parse_line :: proc(line: string) -> (command: Command, pos1: Point, pos2: Point) {
    coordinates_text: string
    if strings.has_prefix(line, TOGGLE_PREFIX) {
        command= .toggle
        coordinates_text = line[len(TOGGLE_PREFIX):]
    } else if strings.has_prefix(line, ON_PREFIX) {
        command = .on
        coordinates_text = line[len(ON_PREFIX):]
    } else if strings.has_prefix(line, OFF_PREFIX) {
        command = .off
        coordinates_text = line[len(OFF_PREFIX):]
    }

    pos1_text, _, pos2_text := strings.partition(coordinates_text, THROUGH)
    pos1 = word_to_point(pos1_text)
    pos2 = word_to_point(pos2_text)
    return
}

main :: proc() {
    data, _ := os.read_entire_file("input", context.allocator)
    defer delete (data, context.allocator)

    lights := make([]int, 1000 * 1000)
    defer delete(lights)

    light_count := 0
    it := string(data)
    for line in strings.split_lines_iterator(&it) {
        command, pos1, pos2 := parse_line(line)

        change := 0
        switch command {
        case .toggle:
            change = 2
        case .on:
            change = 1
        case .off:
            change = -1
        }
        
        for y in pos1.y..=pos2.y {
            col_index := y * 1000
            for x in pos1.x..=pos2.x {
                lights[col_index + x] = clamp(lights[col_index + x] + change, 0, max(int))
            }
        }
    }
    fmt.println(math.sum(lights))

    /*lights := make([]bool, 1000 * 1000)
    defer delete(lights)

    it := string(data)
    for line in strings.split_lines_iterator(&it) {
        command, pos1, pos2 := parse_line(line)

        switch command {
        case .toggle:
            for y in pos1.y..=pos2.y {
                col_index := y * 1000
                for x in pos1.x..=pos2.x {
                    index := col_index + x
                    lights[index] = !lights[index]
                }
            }
        case .on, .off:
            value := command == .on
            for y in pos1.y..=pos2.y {
                col_index := y * 1000
                slice.fill(lights[col_index + pos1.x : col_index + pos2.x + 1], value)
            }
        }
    }
    fmt.println(slice.count(lights, true))*/
}