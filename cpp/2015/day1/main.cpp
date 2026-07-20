#include <fstream>
#include <iostream>


int main() {
    int floor = 0;
    int pos = 0;
    std::ifstream file{"text.txt"};
    char ch;

    while (file.get(ch)) {
        if (ch == '(') floor++;
        else if (ch == ')') floor--;
        pos++;
        if (floor < 0) break;
    }

    file.close();

    std::cout << floor << '\n';
    std::cout << pos << '\n';

}