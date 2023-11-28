#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
using namespace std;

int main() {
    ifstream textFile("text.txt");
    string input;
    int total = 0;
    while (getline(textFile, input)) {
        string str[4] = {"","","",""};
        int i[4];
        int min = 0;
        for(char& c : input) {
            int cur = 0;
            if (c != 'x') {
                str[cur] = str[cur] + c;
            } else {
                i[cur] = to_string(str[cur]);
            }
        }
    }



    textFile.close();


}