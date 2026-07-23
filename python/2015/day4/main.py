import hashlib

INPUT = "yzbqklnj"

num = 1
part_one_found = False
part_two_found = False

while not (part_one_found and part_two_found):
    candidate = (INPUT + str(num)).encode()
    digest = hashlib.md5(candidate).digest()

    if not part_one_found:
        if digest.startswith(b"\x00\x00") and (digest[2] & 0xF0 == 0):
            print("Part 1:", num)
            part_one_found = True

    if not part_two_found:
       if digest.startswith(b"\x00\x00\x00"):
           print("Part 2:", num)
           part_two_found = True           

    num += 1