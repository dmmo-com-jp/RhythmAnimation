using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhythmAnimation;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Controls;
using YukkuriMovieMaker.Exo;
using YukkuriMovieMaker.Player.Video;
using YukkuriMovieMaker.Plugin;
using YukkuriMovieMaker.Plugin.Effects;

namespace RhythmAnimation
{
    /// <summary>
    /// 映像エフェクト
    /// 映像エフェクトには必ず[VideoEffect]属性を設定してください。
    /// </summary>
    [VideoEffect("リズム拡大縮小", ["アニメーション"], [])]
    internal class RhythmScaling : VideoEffectBase
    {
        /// <summary>
        /// エフェクトの名前
        /// </summary>
        public override string Label => "リズム拡大縮小";

        /// <summary>
        /// アイテム編集エリアに表示するエフェクトの設定項目。
        /// [Display]と[AnimationSlider]等のアイテム編集コントロール属性の2つを設定する必要があります。
        /// [AnimationSlider]以外のアイテム編集コントロール属性の一覧はSamplePropertyEditorsプロジェクトを参照してください。
        /// </summary>
        [Display(Name = "BPM", Description = "BPM")]
        [AnimationSlider("F0", "BPM", 0, 250)]
        public Animation BPM { get; } = new Animation(0, -10000, 10000);
        [Display(Name = "X", Description = "X")]
        [AnimationSlider("F2", "px", -10, 10)]
        public Animation X { get; } = new Animation(0, -10000, 10000);
        [Display(Name = "Y", Description = "Y")]
        [AnimationSlider("F2", "px", -10, 10)]
        public Animation Y { get; } = new Animation(0, -10000, 10000);
        /// <summary>
        /// Exoフィルタを作成する。
        /// </summary>
        /// <param name="keyFrameIndex">キーフレーム番号</param>
        /// <param name="exoOutputDescription">exo出力に必要な各種情報</param>
        /// <returns></returns>
        public override IEnumerable<string> CreateExoVideoFilters(int keyFrameIndex, ExoOutputDescription exoOutputDescription)
        {
            //サンプルはSampleD2DVideoEffectを参照
            return [];
        }

        /// <summary>
        /// 映像エフェクトを作成する
        /// </summary>
        /// <param name="devices">デバイス</param>
        /// <returns>映像エフェクト</returns>
        public override IVideoEffectProcessor CreateVideoEffect(IGraphicsDevicesAndContext devices)
        {
            return new RhythmScalingProcessor(this);
        }

        /// <summary>
        /// クラス内のIAnimatableを列挙する。
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<IAnimatable> GetAnimatables() => [X, Y];
        public PluginDetailsAttribute Details => new()
        {
            //制作者
            AuthorName = "メタロロ",
            //作品ID
            ContentId = "",
        };
    }

}
