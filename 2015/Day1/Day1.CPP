#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
using namespace std;

int main() {
    ifstream textFile("text.txt");
    string input;
    ostringstream ss;
    ss << textFile.rdbuf();
    input = ss.str();
    textFile.close();

    int i = 0;
    int j = 0;

    for (char& c : input) {
        /*if (c == '(') {
            i++;
        } else if (c == ')') {
            i--;
        }*/

        if (c == '(') {
            i++;
        } else if (c == ')') {
            i--;
        }
        j++;
        if (i == -1) {
            break;
        }

    }

    cout << j;
}