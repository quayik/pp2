#include<iostream>
#include<vector>

using namespace std;

vector<int> g[100];
int n, m, x, y;

int main(){
    cin >> n >> m;
    for(int i = 0; i < m; ++i){
        cin >> x >> y;
        x--;
        y--;
        g[x].push_back(y);
        g[y].push_back(x);
    }
    return 0;
}
