#include <iostream>
#include <vector>
#include <math.h>
using namespace std;

struct MinHeap
{
    vector<int> v;
    int root;

    MinHeap(){
        v = vector<int>();
        root = 0;
    }

    int parent(int i){
        return (i - 1) / 2;
    }

    int left(int i){
        return 2 * i + 1;
    }

    int right(int i){
        return 2 * i + 2;
    }

    void insert(int a){
        v.push_back(a);
        int i = v.size() - 1;
        while(i > 0 && a < v.at(parent(i))){
            swap(v.at(i), v.at(parent(i)));
            i = parent(i);
        }
    }

    int getMin(){
        return v[0];
    }

    void heapify(int i){
        if (left(i) > v.size() - 1)
            return;
        
        int minChild = left(i);
        int mini = v[left(i)];

        if (right(i) < v.size() && v[right(i)] < mini) {
            mini = v[right(i)];
            minChild = right(i);
        }

        if (v[i] > v[minChild]) {
            swap(v[i], v[minChild]);
            heapify(minChild);
        }
    }

    int extractMin(){
        int min = v.front();
        swap(v[0], v[v.size() - 1]);
        v.pop_back();

        heapify(0);

        return min;
    }

    void decreaseKey(int i, int a){
        v[i] = a;
        while (i > 0 && v[i] < v[parent(i)]){
            swap(v.at(i), v.at(parent(i)));
            i = parent(i);
        }
    }

    void incKey(int i, int a){
        v[i] = a;
        heapify(i); 
    }

    void del(int i){
        this->decreaseKey(i, v[0] - 1);
        extractMin();
    }

    void sorted(){
        MinHeap m = *this;
        for (int i = 0; i < v.size(); i++) {
        cout << m.extractMin() << " ";
        }
        cout << endl;
    }
    
};

int main(){
    MinHeap mh = MinHeap();
    int n, x;
    cin >> n;
    for (int i = 0; i < n; i++){
        cin >> x;
        mh.insert(x);
    }
    mh.sorted();
    int i, a;
    cout << "enter index and number" << endl;
    cin >> i >> a;
    mh.incKey(i, a);
    mh.sorted();

    return 0;
}