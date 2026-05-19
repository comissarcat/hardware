namespace Hardware
{
    internal class TreeViewStateRestorer
    {
        public static HashSet<string> GetExpandedPaths(TreeView tree)
        {
            HashSet<string> paths = [];
            CollectExpandedPaths(tree.Nodes, "", paths);
            return paths;
        }

        private static void CollectExpandedPaths(TreeNodeCollection nodes, string parentPath, HashSet<string> paths)
        {
            foreach (TreeNode node in nodes)
            {
                string path = string.IsNullOrEmpty(parentPath) ? node.FullPath : node.FullPath;

                if (node.IsExpanded)
                    paths.Add(path);

                CollectExpandedPaths(node.Nodes, path, paths);
            }
        }

        public static void RestoreExpandedPaths(TreeView tree, HashSet<string> paths)
        {
            RestoreExpandedPathsRecursive(tree.Nodes, paths);
        }

        private static void RestoreExpandedPathsRecursive(TreeNodeCollection nodes, HashSet<string> paths)
        {
            foreach (TreeNode node in nodes)
            {
                if (paths.Contains(node.FullPath))
                    node.Expand();

                RestoreExpandedPathsRecursive(node.Nodes, paths);
            }
        }

        public static (string? node, string? parent) GetSelectedPath(TreeView tree)
        {
            return (tree.SelectedNode?.FullPath, tree.SelectedNode?.Parent?.FullPath);
        }

        public static void RestoreSelectedPath(TreeView tree, string? path, string? parentPath)
        {
            if (string.IsNullOrEmpty(path))
                return;

            TreeNode? selectedNode = FindNodeByPath(tree.Nodes, path);

            if (selectedNode != null)
            {
                tree.SelectedNode = selectedNode;
                tree.SelectedNode.EnsureVisible();
            }
            else
            {
                if (string.IsNullOrEmpty(parentPath))
                    return;
                selectedNode = FindNodeByPath(tree.Nodes, parentPath);
                if (selectedNode != null)
                {
                    tree.SelectedNode = selectedNode;
                    tree.SelectedNode.EnsureVisible();
                }
            }
        }

        private static TreeNode? FindNodeByPath(TreeNodeCollection nodes, string path)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.FullPath == path)
                    return node;

                TreeNode? found = FindNodeByPath(node.Nodes, path);
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}
