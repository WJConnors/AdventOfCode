#include <algorithm>
#include <fstream>
#include <iostream>

int main() {
	std::ifstream file{"text.txt"};

	int l, w, h;
	char x;

	int total = 0;

	while (file >> l >> x >> w >> x >> h) {
		int lw = l * w;
		int lh = l * h;
		int wh = w * h;

		total += 2 * (lw + lh + wh) + std::min({lw, lh, wh});
	}

	std::cout << total << '\n';
}
