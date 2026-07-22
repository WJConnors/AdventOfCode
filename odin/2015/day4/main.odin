package main

import "core:fmt"
import "core:crypto/hash"
import "core:strconv"

input :: "yzbqklnj"

main :: proc() {
    in_buffer: [32]u8
    out_buffer: [16]u8
    num: i64 = 1

    input_len := copy(in_buffer[:], input)

    for {
        num_len := len(strconv.write_int(in_buffer[input_len:], num, 10))
        hash.hash_bytes_to_buffer(
            .Insecure_MD5,
            in_buffer[:input_len + num_len],
            out_buffer[:]
        )

        if (
            out_buffer[0] == 0 &&
            out_buffer[1] == 0 &&
            //(out_buffer[2] & 0xF0) == 0
            out_buffer[2] == 0
        ) {
            break
        }

        num += 1
    }
    fmt.println(num)
}