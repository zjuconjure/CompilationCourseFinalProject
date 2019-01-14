// micro-C example 1

void main(int n) {
  n=10;
  {
    print n;
    n = n - 1;
  }
until( n > 0 )
  println;
}
