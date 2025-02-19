using Vortice.Direct2D1;
using Vortice.DirectWrite;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Player.Video;

namespace RhythmAnimation
{
    internal class RhythmPositionProcessor : IVideoEffectProcessor
    {
        readonly RhythmPosition item;
        ID2D1Image? input;

        public ID2D1Image Output => input ?? throw new NullReferenceException(nameof(input) + "is null");

        public RhythmPositionProcessor(RhythmPosition item)
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
            var BPM = item.BPM.GetValue(frame, length, fps);
            var drawDesc = effectDescription.DrawDescription;
            var 拡大間隔 = Math.Round(1 / (BPM / 60.0) / (1.0 / fps));
            var fps_frame = frame % 拡大間隔;

            return
                drawDesc with
                {
                    Draw = new(
                        drawDesc.Draw.X + (((float)x / (float)拡大間隔) * (float)fps_frame),
                        drawDesc.Draw.Y + (((float)y / (float)拡大間隔) * (float)fps_frame),
                        drawDesc.Draw.Z
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
