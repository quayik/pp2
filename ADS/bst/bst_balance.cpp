#include <iostream>
#include<cmath>

using namespace std;

class Node {
    public:
    int data;
    Node *right;
    Node *left;

    Node(int data) {
        this->data = data;
        right = NULL;
        left = NULL;
    }
};

class BST
{
    public:
    Node *root;
    int d;
    bool balanced;

    BST() {
        root = NULL;
        d = 0;
        balanced = true;
    }

    Node *insert(Node *node, int data){
        if (node == NULL){
            node = new Node(data);
            return node;
        }
        if(data <= node->data)
            node->left = insert(node->left, data);
        else
            node->right = insert(node->right, data);
            return node;
    }

    bool exists(Node *node, int data){
        if (node == NULL)
            return false;
        else if(data == node->data) return true;
        else return (exists(node->left, data) || exists(node->right, data));
    }

    int depth(Node *node){
        if (node ==NULL)
            return 0;
        else if((node->right != NULL || node->left != NULL))
            return max(depth(node->right), depth(node->left)) + 1;
        else if(node->right == NULL && node->left == NULL) return 1;
        return d - 1;
    }

    void check(Node *node) {
        if (node == NULL)
            return ;
        
        if(balanced){
            check(node->left);
            balanced = abs( depth(node->left) - depth(node->right)) < 2;
            check(node->right);
        }
    }

   
};

int main(){
    BST *bst = new BST();
    int n = 1;

    while(true){
        cin >> n;
        if (n == 0) break;
        if(!bst->exists(bst->root, n))
            bst->root = bst->insert(bst->root, n);
    }

    bst->check(bst->root);
    if(bst->balanced)
        cout << "YES";
    else cout << "NO";
    return 0;
}