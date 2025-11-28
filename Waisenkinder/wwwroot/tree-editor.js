// Lightweight drag-and-drop for SVG nodes
(function () {
    window.TreeEditor = {
        init: function (svgId) {
            const svg = document.getElementById(svgId);
            if (!svg) return;

            let selected = null;
            let offset = { x: 0, y: 0 };

            function getPoint(evt) {
                const pt = svg.createSVGPoint();
                pt.x = evt.clientX; pt.y = evt.clientY;
                const ctm = svg.getScreenCTM().inverse();
                const p = pt.matrixTransform(ctm);
                return p;
            }

            svg.addEventListener('mousedown', function (e) {
                const target = e.target.closest && e.target.closest('.node');
                if (!target) return;
                selected = target;
                const p = getPoint(e);
                const transform = selected.getAttribute('transform') || 'translate(0,0)';
                const match = /translate\(([-0-9.]+),\s*([-0-9.]+)\)/.exec(transform);
                let tx = 0, ty = 0;
                if (match) { tx = parseFloat(match[1]); ty = parseFloat(match[2]); }
                offset.x = p.x - tx;
                offset.y = p.y - ty;
                e.preventDefault();
            });

            window.addEventListener('mousemove', function (e) {
                if (!selected) return;
                const p = getPoint(e);
                const nx = p.x - offset.x;
                const ny = p.y - offset.y;
                selected.setAttribute('transform', 'translate(' + nx + ',' + ny + ')');
            });

            window.addEventListener('mouseup', function () { selected = null; });
        },
        getSvgHtml: function (svgId) {
            const svg = document.getElementById(svgId);
            if (!svg) return '';
            // Inline computed styles by cloning and serializing
            const clone = svg.cloneNode(true);
            // add xmlns if missing
            if (!clone.getAttribute('xmlns')) clone.setAttribute('xmlns', 'http://www.w3.org/2000/svg');
            return clone.outerHTML;
        }
    };
    // helper to trigger downloads from data URLs or blob URLs
    window.TreeEditor.download = function (href, filename) {
        var a = document.createElement('a');
        a.href = href;
        a.download = filename || 'download';
        document.body.appendChild(a);
        a.click();
        a.remove();
    };

    // go back in browser history
    window.TreeEditor.goBack = function () {
        if (history && history.length > 1) history.back();
    };
})();
