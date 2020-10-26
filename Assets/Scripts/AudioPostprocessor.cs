using System.IO;
using UnityEditor;
using UnityEngine;

public class AudioPostprocessor : AssetPostprocessor
{
    private void OnPreprocessAudio()
    {
        const int minSize = 200;
        const int maxSize = 2000;
        var fileSize = new FileInfo(assetPath).Length/1024;
        var audioImporter = assetImporter as AudioImporter;

        if (audioImporter is null) return;
        
        audioImporter.loadInBackground = true;
        audioImporter.preloadAudioData = true;

        var audioImporterDefaultSampleSettings = audioImporter.defaultSampleSettings;

        audioImporterDefaultSampleSettings.loadType = fileSize < minSize ? AudioClipLoadType.DecompressOnLoad :
            fileSize >= minSize && fileSize <= maxSize ? AudioClipLoadType.CompressedInMemory :
            AudioClipLoadType.Streaming;

        audioImporter.defaultSampleSettings = audioImporterDefaultSampleSettings;
    }
}