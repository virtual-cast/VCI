namespace VCIGLTF
{
    public class RootAnimationImporter : IAnimationImporter
    {
        public void Import(ImporterContext context)
        {
            AnimationImporterUtil.ImportAnimation(context);
        }
    }
}