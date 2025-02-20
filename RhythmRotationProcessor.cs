using Vortice.Direct2D1;
using Vortice.DirectWrite;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Player.Video;

namespace RhythmAnimation
{
    internal class RhythmRotationProcessor : IVideoEffectProcessor
    {
        readonly RhythmRotation item;
        ID2D1Image? input;

        public ID2D1Image Output => input ?? throw new NullReferenceException(nameof(input) + "is null");

        public RhythmRotationProcessor(RhythmRotation item)
        {
            this.item = item;
        }

        /// <summary>
        /// エフェクトを更新する
        /// </summary>
        /// <param name="effectDescription">エフェクトの描画に必要な各種情報</param>
        /// <returns>描画位置等の情報</returns>
        public DrawDescription Update(EffectDescription effectDescription)
        {
            var frame = effectDescription.ItemPosition.Frame;
            var length = effectDescription.ItemDuration.Frame;
            var fps = effectDescription.FPS;
            var x = item.X.GetValue(frame, length, fps);
            var y = item.Y.GetValue(frame, length, fps);
            var z = item.Z.GetValue(frame, length, fps);
            var BPM = item.BPM.GetValue(frame, length, fps);
            var drawDesc = effectDescription.DrawDescription;
            var 拡大間隔 = Math.Round(1 / (BPM / 60.0) / (1.0 / fps));
            var fps_frame = frame % 拡大間隔;

            return
                drawDesc with
                {
                    Rotation = new(
                        drawDesc.Rotation.X + ((int)x /(int)拡大間隔)*(int)fps_frame,
                        drawDesc.Rotation.Y + ((int)y / (int)拡大間隔) * (int)fps_frame,
                        drawDesc.Rotation.Z + ((int)z / (int)拡大間隔) * (int)fps_frame
                        )
                };
        }
        public void ClearInput()
        {
            input = null;
        }
        public void SetInput(ID2D1Image? input)
        {
            this.input = input;
        }

        public void Dispose()
        {

        }

    }
}
