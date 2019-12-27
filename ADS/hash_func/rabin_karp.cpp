#include<iostream>
#include<vector>

using namespace std;



vector<long long> hsh(string s){
    int p = 31;
    int sz = s.size();
    vector<long> p_pow(sz);
    vector<long long> h(sz);
    p_pow[0] = 1;

    for(int i = 1; i < sz; i++)
        p_pow[i] = p_pow[i - 1] * p;
    
    for(int i = 0; i < sz; i++){
        h[i] = (s[i] - 'a' + 1) * p_pow[i];
        if(i) h[i] += h[i - 1];
    }
//     for (size_t i=0; i<t.length(); ++i)
// {
// 	h[i] = (t[i] - 'a' + 1) * p_pow[i];
// 	if (i)  h[i] += h[i-1];
// }

    return h;
}


int main(){
    string s, t;
    cin >> s >> t;
    //vector<long long> x = hash(s);
    // for(int i = 0; i < x.size(); i++)
    //     cout << x[i] << " ";
    vector<long> p_pow(s.size());
    p_pow[0] = 1;
    for(int i = 1; i < s.size(); i++)
        p_pow[i] = p_pow[i - 1] * 31;
    
    long long h_s = 0;
    for (int i=0; i<s.length(); ++i)
	        h_s += (s[i] - 'a' + 1) * p_pow[i];
    vector<long long> ht = hsh(t);
    int x = s.size();
    for(int i = 0; i < t.size() - x - 1; i++){
        long long hx = ht[i + x - 1];
        if(i) hx -= ht[i - 1];
        if (hx == h_s * p_pow[i]) 
            cout << i << " ";
    }

    return 0;
}