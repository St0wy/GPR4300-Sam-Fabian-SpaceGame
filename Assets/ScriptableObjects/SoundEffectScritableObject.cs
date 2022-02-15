using System;
using JetBrains.Annotations;
using MyBox;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceGame.ScriptableObjects
{
	/// <summary>
	/// Sound effect system based on this video https://www.youtube.com/watch?v=xDLqdZu0ll0.
	/// </summary>
	[CreateAssetMenu(fileName = "soundEffect", menuName = "Audio/Sound Effect", order = 0)]
	public class SoundEffectScritableObject : ScriptableObject
	{
		[SerializeField] private AudioClip[] clips;

		[MinMaxRange(0, 1)]
		[SerializeField]
		private RangedFloat volume = new RangedFloat(0.5f, 0.5f);

		[MinMaxRange(0, 3)]
		[SerializeField]
		private RangedFloat pitch = new RangedFloat(1, 1);

		[SerializeField] private SoundClipPlayOrder playOrder;

		[Attributes.ReadOnly]
		[SerializeField]
		private int playIndex;

		/// <summary>
		/// Gets a random element from the <see cref="clips"/> array.
		/// </summary>
		private AudioClip NextAudioClip
		{
			get
			{
				// Get current clip
				AudioClip clip = clips[playIndex >= clips.Length ? 0 : playIndex];

				// Find id for next clip
				playIndex = playOrder switch
				{
					SoundClipPlayOrder.Random => Random.Range(0, clips.Length),
					SoundClipPlayOrder.InOrder => (playIndex + 1) % clips.Length,
					SoundClipPlayOrder.Reverse => (playIndex + clips.Length - 1) % clips.Length,
					_ => throw new ArgumentOutOfRangeException(),
				};

				return clip;
			}
		}

		#region Preview

#if UNITY_EDITOR_WIN
		private AudioSource previewer;

		private void OnEnable()
		{
			previewer = EditorUtility.CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave,
				typeof(AudioSource)).GetComponent<AudioSource>();
		}

		private void OnDisable()
		{
			DestroyImmediate(previewer.gameObject);
		}

		[UsedImplicitly]
		[ButtonMethod]
		private void PlayPreview()
		{
			Play(previewer);
		}

		[UsedImplicitly]
		[ButtonMethod]
		private void StopPreview()
		{
			previewer.Stop();
		}
#endif

		#endregion

		public AudioSource Play(AudioSource audioSourceParam = null)
		{
			if (clips.Length == 0)
			{
				Debug.LogWarning($"Missing audio clips for {name}");
				return null;
			}

			if (audioSourceParam == null)
			{
				var obj = new GameObject("Sound", typeof(AudioSource));
				audioSourceParam = obj.GetComponent<AudioSource>();
			}

			// Set source config
			audioSourceParam.clip = NextAudioClip;
			audioSourceParam.volume = Random.Range(volume.Min, volume.Max);
			audioSourceParam.pitch = Random.Range(pitch.Min, pitch.Max);

			audioSourceParam.Play();

#if UNITY_EDITOR
			if (audioSourceParam != previewer)
#endif
			{
				Destroy(audioSourceParam.gameObject, audioSourceParam.clip.length / audioSourceParam.pitch);
			}

			return audioSourceParam;
		}
	}
}
