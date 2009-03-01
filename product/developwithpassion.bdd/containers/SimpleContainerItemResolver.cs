namespace developwithpassion.bdd.containers
{
    public class SimpleContainerItemResolver : IContainerItemResolver
    {
        readonly DependencyResolver resolver;

        public SimpleContainerItemResolver(DependencyResolver resolver)
        {
            this.resolver = resolver;
        }

        public object resolve()
        {
            return resolver();
        }
    }
}