namespace VCIGLTF
{
    public sealed class RootAnimationImporter : IAnimationImporter
    {
        public void Import(ImporterContext context)
        {
            AnimationImporterUtil.ImportAnimation(context);
        }
    }
}