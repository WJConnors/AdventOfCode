#include <unordered_map>
#include <vector>
#include <iostream>

class Solution {
public:
    std::vector<int> twoSum(std::vector<int>& nums, int target) {
        std::unordered_map<int, int> m;
        for (std::size_t idx = 0; idx < nums.size(); idx++ ) {
            const auto match = m.find(target - nums[idx]);
            if (match != m.end()) {
                return {static_cast<int>(idx), match->second};
            }

            m.emplace(nums[idx], static_cast<int>(idx));
        }
        return {};
    }
};

int main() {
    Solution sol;
    std::vector<int> nums = {3,2,4};
    auto result = sol.twoSum(nums, 6);
    for (auto i : result) {
        std::cout << i << '\n';
    }
}