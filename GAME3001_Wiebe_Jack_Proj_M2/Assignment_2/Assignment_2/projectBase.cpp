#include<iostream>
#include<string>

using namespace std;


/*

Coeded By:

Jack Wiebe - 101031347	

Evan Grinbergs - 100991709

Last Edit 12/05/2016

*/


class Weapon{
public:
    string weaponName;
    int range;
    int damage;
    float weight;
    float cost;

    Weapon(string n,int rang,int dam,float w,float c){
        weaponName=n;
        damage=dam;
        range=rang;
        weight = w;
        cost=c;
    }


	 
	void printData()
	{
		cout<<"Name: "<<weaponName<<"   Damage: "<<damage<<"    Cost: "<< cost <<endl;
		//cout << " " << weaponName << endl;
	}

	bool compareGreater(Weapon* other){
		if(this->weaponName < other->weaponName){
			return false;
		}
		else{
			return true;
		}
	}

	bool operator> (const Weapon* other)
	{
		if(this->weaponName < other->weaponName)
		{
			return true;
		}
		else{
			return false;
		}
	}

	bool operator< ( const Weapon* other)
	{
		return this->weaponName < other->weaponName;
		
	}
};

template<class T> class Node{
public:
    T* data;
    Node<T>* left;
	Node<T>* right;
    Node (T* value){
        data=value;
        left=NULL;
		right=NULL;
    }

};

template <class T> class BinaryTree{
 public:
    Node<T>* root;
	BinaryTree(){
        root=NULL;
    }

	 void push(T* x){
		
		Node<T>* current = NULL;
		//Node<T>* parent = NULL;
		Node<T>* y = new Node<T>(x); // creates a new node,y is a pointer to this node.
		if(root==NULL)
		{
			root = y;
			return;
		}
		else 
		{
			current = root;
		}
		
		
		while(1)
		{
			if(current->data->compareGreater(x)){
				if(current->left == NULL)
				{
					current->left = y;
					return;
				}
				else{
					current = current->left;
				}
			}else{
				if(current->right == NULL)
				{
					current->right = y;
					return;
				}
				else{
					current = current->right;
				}
			}
		}
	 }

	 void display()
	 {
		 displayInOrder(root);
	 }

	 void displayInOrder(Node<T>* node)
	 {
		 if(node!=NULL){
			 displayInOrder(node->left);
			// cout << node->data << " ";

			 node->data->printData();
			 displayInOrder(node->right);

		 }
	 }

	 void printList(){
		Node<T>* curr=head; //sets a new pointer variable to whatever head is pointing to.
		while(curr!=NULL){
			cout<<curr->data<<" ";
			curr=curr->next; // changes the value of "present" to whatever the "next" is pointing to
		}
		cout<<endl;
	 }

	 void insertInorder(T x){
		 Node<T>* curr=head;
		 Node<T>* prev=head;
		 Node<T>* y= new Node<T>(x);
		 if (head==NULL){  // case 1 list is empty
			head = y;
			tail = y;
		 }else{
			if(x<=head->data){ //case 2 x is the smallest item
				y->next=head;
				head=y;
			}else if(x >= tail->data){ //case 3 x is the largest item
				tail ->next = y;
				tail = y;
			}else{   // case 4 middle of list
				while (curr!= NULL && curr->data<x){
					prev=curr;
					curr=curr->next;
				}
				y->next=curr;
				prev->next=y;
			}
		 }
	 }

	 void remove(T* data)
	 {
		 if(root == NULL)
			 return;

		 Node<T>* parent = root;
		 Node<T>* node = root;
		 bool isLeftNode = false;

		 while(node->data != data)
		 {
			 parent = node;
			 if(node->data->compareGreater(data))
			 {
				 node = node->left;
				 isLeftNode = true;
			 }else
			 {
				 node = node->right;
				 isLeftNode = false;
			 }
			 if(node == NULL)
				 return;
		 }

		 //case last entry in tree
		 if(node->left == NULL && node->right == NULL)
		 {
			 if(node == root) {
				 root = NULL;	//case delete only node in tree
			 }else if(isLeftNode){
				 parent->left = NULL;
			 }else {
				 parent->right = NULL;
			 }
		 }else if(node->left == NULL)
		 {
			 if(node == root) {
				 root = node->right;	//case delete only node in tree
			 }else if(isLeftNode){
				 parent->left = node->right;
			 }else {
				 parent->right = node->right;
			 }
		 }else if(node->right == NULL)
		 {
			  if(node == root) {
				 root = node->left;	//case delete only node in tree
			 }else if(isLeftNode){
				 parent->left = node->left;
			 }else {
				 parent->right = node->left;
			 }
		 }else
		 {
			 Node<T>* tempNode = node->right;
			 Node<T>* successor = node;
			 Node<T>* successorParent = node;

			 while(tempNode != NULL)
			 {
				 successorParent = successor;
				 successor = tempNode;
				 tempNode = tempNode->left;
			 }
			 if(successor != node->right)
			 {
				 successorParent->left = successor->right;
				 successor->right = node->right;
			 }
			 if(node == root)
			 {
				 root = successor;
			 }
			 else if(isLeftNode)
			 {
				 node = parent->left;
				 parent->left = successor;
			 }else
			 {
				 node = parent->right;
				 parent->right = successor;
			 }

			 successor->left = node->left;


		 }

		 node->left = NULL;
		 node->right = NULL;
		 delete node;
	
	 }

	 void deleteNode (T weapon){
		 Node<T>* curr=root;
		 Node<T>* prev=root;
		 // case 1 list is empty
		 if (curr!=NULL){ // case 1 list is empty, if it is NUll then nothing to do

			if (curr->data==weapon){//case 2  item to be deleted is first item
				root= curr->next;
				delete curr;
			}
			else{// Case 3 item is in the middle or at end
				while (curr!=NULL && curr->data!=weapon){ // goes until it has found it or reaches the end
					prev=curr;
					curr=curr->next;
				}
				if (curr!=NULL){ // if found within the list
					prev->next=curr->next;
					if(curr->next == NULL)//Case 4 item is the last in list
						//ail = prev;
					delete curr;
				}
			}

		 }
	 }
	
	Node<Weapon>* get(string name){
	
		Node<Weapon> *current = root;
		
		while(current->data->weaponName != name)
		{
			if(current != NULL)
			{
				if(current->data->weaponName > name){
					current = current->left;
				}else{
					current = current->right;
				}
				if(current == NULL)
				{
					return NULL;
				}
			}
		}
		return current;
	}

	 void addLastOrig(T x){
		Node<T>* present=head;
		Node<T>* y = new Node<T>(x);  // creates a new node,y is a pointer to this node.
		if(present==NULL)  // checks if the list is empty
			head=y;  // if the list is empty set "head" to point to the new node
		else{
			while(present->next!=NULL){// finds the SECOND TO LAST node in the list
				present=present->next;
			}
			present->next=y; // sets the pointer of the last node to point to the new node
		}
	 }
	 void addLast(T x){
		Node<T>* y = new Node<T>(x);  // creates a new node,y is a pointer to this node.
		if(head == NULL)
		{
			head = y;
			tail = y;
			return;
		}
		tail->next = y;
		tail = y;
	  }
 };

/*class hashTable{

public:
    int tableLength;    // records the max size of the table
    int numItems;       // records number of items in the list
    Weapon **table; //hashtable itself

    hashTable(int size){
        tableLength=size;
        numItems=0;
        table = new Weapon*[tableLength];
        for (int i=0;i<tableLength;i++){
            table[i]=NULL;
        }
    }


    int hash( string key )
    {
        int value = 0;
        for ( int i = 0; i < key.length(); i++ )
            value = value + key[i];
        return value % tableLength;
    }

    void put(Weapon *item){
        int location= hash(item->weaponName); //gets location in table based on name
        while (table[location]!=NULL){
            location=(location+1);      // look one down
            location=location%tableLength; // to ensure wraparound at end of array
        }
        table[location]=item;
        numItems++;
    }

    Weapon* get(string key){
        int location= hash(key); //gets location in table based on key
        while (table[location]!=NULL && key.compare(table[location]->weaponName)!=0){  // not empty and not item
            location=(location+1);      // look one down
            location=location%tableLength; // to ensure wraparound at end of array
        }
        return table[location];
    }

    void printTable(){
        int count = 0;
        for (int x=0;x<tableLength;x++){
            if (table[x]!=NULL){
                cout<<"Name: "<<table[x]->weaponName<<"   Damage:"<<table[x]->damage<<"    Cost:"<<table[x]->cost<<endl;
            }
        }
    }
};*/

class Player{
public:
    string name;
    Weapon ** backpack;
    int numItems;
    float money;

    Player (string n,float m){
        name=n;
        money =m;
        numItems=0;
        backpack=new Weapon*[10];
    }

    void buy (Weapon * w){
        cout<<w->weaponName<<" bought..."<<endl;
        backpack[numItems]=w;
        numItems++;
        cout<<numItems << " items in backpack"<<endl;
    }
    void withdraw(float amt){
        money=money-amt;
    }

    bool inventoryFull(){
        bool full= false;
        if (numItems==10)full=true;
        return full;
    }


    void printCharacter(){
        cout<<" Name:"<<name<<"\n Money:"<<money<<endl;
        printBackpack();
    }

    void printBackpack(){
        cout<<" "<<name<<", you own "<<numItems<<" Weapons:"<<endl;
        for (int x=0;x<numItems;x++){
            cout<<" "<<backpack[x]->weaponName<<endl;
        }
        cout<<endl;
    }

};

void addWeapons(BinaryTree<Weapon> *bTree){
    cout<<"***********WELCOME TO THE WEAPON ADDING MENU*********"<<endl;
    string weaponName;int weaponRange;int weaponDamage;float weaponWeight;float weaponCost;
    cout<<"Please enter the NAME of the Weapon ('end' to quit):";cin>>weaponName;
    while (weaponName.compare("end")!=0){
        cout<<"Please enter the Range of the Weapon (0-10):";cin>>weaponRange;
        cout<<"Please enter the Damage of the Weapon:";cin>>weaponDamage;
        cout<<"Please enter the Weight of the Weapon (in pounds):";cin>>weaponWeight;
        cout<<"Please enter the Cost of the Weapon:";cin>>weaponCost;
        Weapon *w= new Weapon(weaponName,weaponRange,weaponDamage,weaponWeight,weaponCost);

        bTree->push(w);
        cout<<"Please enter the NAME of another Weapon ('end' to quit):";cin>>weaponName;
    }
}

void showRoom(BinaryTree<Weapon> *bTree,Player * p){
    string choice;
    cout<<"WELCOME TO THE SHOWROOM!!!!"<<endl;
	bTree->display();
    cout<<" You have "<< p->money<<" money."<<endl;
    cout<<"Please select a weapon to buy('end' to quit):";cin>>choice;
    while (choice.compare("end")!=0&& !p->inventoryFull()){
		Node<Weapon> *temp = bTree->get(choice);
		if(temp==NULL){
			cout<<" ** "<<choice<<" not found!! **"<<endl;
			break;
		}
		Weapon *w= temp->data;
        if (w!=NULL){
            if (w->cost > p->money){
                cout<<"Insufficient funds to buy "<<w->weaponName<<endl;
            }else{
                p->buy(w);
                p->withdraw(w->cost);
				bTree->remove(w);
            }
        }else{
            cout<<" ** "<<choice<<" not found!! **"<<endl;
        }
       
		bTree->display();
		cout<<"Please select another weapon to buy('end' to quit):";cin>>choice;
		
    }
    cout<<endl;
}


int main (){
    string pname;
    cout<<"Please enter Player name:"<<endl;
    cin>>pname;
	Player pl (pname,45);
//  Player * pntPlayer = &pl; 
	BinaryTree<Weapon> bTree;
//	BinaryTree<Weapon> * ptrBTree = &bTree; 

    addWeapons(&bTree);
    showRoom(&bTree,&pl);
    pl.printCharacter();

    return 0;
}