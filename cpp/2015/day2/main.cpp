#include <iostream>
#include <fstream>
#include <string>
#include <sstream>
#include <numeric>
#include <algorithm>

int main() {
    std::ifstream file {"text.txt"};
    std::string line;

    int i[3];
    int s[3];

    int total = 0;

    while (file >> i[0]) {
        file.ignore(1);
        file >> i[1];
        file.ignore(1);
        file >> i[2];

        s[0] = i[0] * i[1];
        s[1] = i[0] * i[2];
        s[2] = i[1] * i[2];

        total += *std::min_element(std::begin(s), std::end(s)) + (2 * (s[0]+s[1]+s[2])); 

    }

    std::cout << total << '\n';
}