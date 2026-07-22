package main

import "core:fmt"
import "core:os"

Pos :: struct {
    x, y: int
}

main :: proc() {
    data, _ := os.read_entire_file("input", context.allocator)
    defer delete(data)

    visited := make(map[Pos]struct{})
    defer delete(visited)

    visited[Pos{}] = {}

    positions: [2]Pos
    turn := 0

    for r in data {
        cur_pos := &positions[turn]
        switch r {
            case '^':
                cur_pos.y += 1
            case '>':
                cur_pos.x += 1
            case 'v':
                cur_pos.y -= 1
            case '<':
                cur_pos.x -= 1
            case:
                continue
        }
        visited[cur_pos^] = {}
        turn = 1 - turn
    }

    fmt.println(len(visited))
}