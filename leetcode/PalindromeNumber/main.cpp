#include <iostream>
#include <string>

class Solution {
public:
    bool isPalindrome(int x) {
        if (x < 0 || (x > 0 && x % 10 == 0)) return false;

        int reversed = 0;
        while (reversed < x) {
            reversed = (reversed * 10) + (x % 10);
            x /= 10;
        }
        return x == reversed || x == reversed / 10;
    }
};

int main() {
    Solution sol;
    std::cout << sol.isPalindrome(121) << '\n';
    std::cout << sol.isPalindrome(-121) << '\n';
    std::cout << sol.isPalindrome(10) << '\n';
    std::cout << sol.isPalindrome(1221) << '\n';
    std::cout << sol.isPalindrome(12321) << '\n';
    std::cout << sol.isPalindrome(0) << '\n';
}
