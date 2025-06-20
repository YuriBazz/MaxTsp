namespace TspTask;

public class MaxWeightMatching {
    int N;
    double[,] W;
    int[] mate, label, parent, baseV;
    bool[] inQueue;
    int[,] adj;
    Queue<int> queue;
    private EGraph graph;

    public MaxWeightMatching(EGraph gr) {
        N = gr.N;
        W = gr.Weights;
        mate = new int[N];
        label = new int[N];
        parent = new int[N];
        baseV = new int[N];
        inQueue = new bool[N];
        adj = new int[N,N];
        queue = new Queue<int>();
        for(int i=0;i<N;i++){
            mate[i] = -1;
            baseV[i] = i;
            for(int j=0;j<N;j++){
                if(W[i,j]>0) adj[i,j]=1;
            }
        }

        graph = gr;
    }

    public List<Edge> Solve() {
        for(int i=0;i<N;i++){
            if(mate[i] < 0)
                Augment(i);
        }
        var res = new List<Edge>();
        for(int i=0;i<N;i++){
            if(mate[i]>i)
                res.Add(new Edge(graph.Vertexes[i],graph.Vertexes[mate[i]], W[i,mate[i]]));
            if(mate[i] == -1) res.Add(new Edge(graph.Vertexes[i],graph.Vertexes[i],-1));
        }
        return res;
    }

    void Augment(int root) {
        Array.Clear(label,0,N);
        Array.Fill(parent, -1);
        for(int i=0;i<N;i++) baseV[i] = i;
        queue.Clear();
        queue.Enqueue(root);
        inQueue[root] = true;
        label[root] = 1;

        while(queue.Count>0){
            int v = queue.Dequeue();
            for(int u=0; u<N; u++){
                if(adj[v,u]==0 || baseV[v]==baseV[u] || label[u]==2) continue;
                if(label[u]==1){
                    int b = LCA(v,u);
                    MarkPath(v,u,b);
                    MarkPath(u,v,b);
                    for(int i=0;i<N;i++){
                        if(label[baseV[i]]==2 && baseV[i]==baseV[baseV[i]]){
                            label[i]=1; queue.Enqueue(i);
                        }
                    }
                }
                else if(mate[u]==-1){
                    parent[u]=v;
                    AugmentPath(u);
                    return;
                }
                else {
                    parent[u]=v;
                    label[u]=2;
                    int m = mate[u];
                    label[m]=1;
                    queue.Enqueue(m);
                }
            }
        }
    }

    int LCA(int a, int b){
        bool[] used = new bool[N];
        while(true) {
            a = baseV[a];
            used[a] = true;
            if(mate[a] < 0) break;
            a = parent[mate[a]];
        }
        while(true){
            b = baseV[b];
            if(used[b]) return b;
            b = parent[mate[b]];
        }
    }

    void MarkPath(int v, int u, int b){
        while(baseV[v]!=b){
            label[baseV[v]] = label[baseV[mate[v]]] = 2;
            parent[v] = u;
            u = mate[v];
            v = parent[u];
        }
    }

    void AugmentPath(int u){
        while(u>=0){
            int v = parent[u];
            int next = mate[v];
            mate[v]=u;
            mate[u]=v;
            u = next;
        }
    }
}