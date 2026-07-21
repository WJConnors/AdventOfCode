with open("text.txt", "r") as file:
    line = file.readline().strip()
#floor = line.count('(') - line.count(')')

floor = 0
for i, char in enumerate(line, start=1):
    floor += 1 if char == '(' else -1
    if floor < 0:
        break

print(floor)
print(i)