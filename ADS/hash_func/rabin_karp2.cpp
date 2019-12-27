#include<iostream>
#include<vector>

using namespace std;

int main(){
    //vector<long long> vs, vt;
    string s, t;
    cin >> s >> t;
    int s_sz = s.size();
    int t_sz = t.size();
    vector<long long> pow(t_sz);
    pow[0] = 1;

    for(int i = 1; i < t_sz; ++i){
        pow[i] = pow[i - 1] * 31;
    }

    long long s_h = 0;
    
    for(int i = 0; i < s_sz; ++i){
        s_h += (s[i] - 'a' + 1) * pow[i];
    }

    vector<long long> hashes(t_sz);

    for(int i = 0; i < t_sz; ++i){
        hashes[i] = (t[i] - 'a' + 1) * pow[i];
        if(i != 0) hashes[i] += hashes[i - 1];
    }

    long long hx = 0;
    for(int i = 0; i < t_sz - s_sz + 1; ++i){
        int j = i + s_sz - 1;
        hx = hashes[j];
        if(i != 0){
            hx -= hashes[i - 1];
        }

        if(s_h * pow[i] == hx)
            cout << i << " ";
    }

    // 0 1 2 3 4 5
    //

    // cout << s_h << endl;

    // for(int i = 0; i < t_sz; ++i)
    //     cout << hashes[i] << " ";
    // for(int i = 0; i < t_sz; ++i)
    //     cout << pow[i] << " ";
    // return 0;
}