totalArea = 0
totalLength = 0

with open("text.txt", "r") as file:
    for line in file:
        l,w,h = map(int, line.split('x'))
        areas = [l*w,l*h,w*h]
        totalArea +=  (2 * sum(areas)) + min(areas)

        totalLength += (2 * min(l+w,l+h,w+h)) + (l*w*h)

print(totalArea)
print(totalLength)