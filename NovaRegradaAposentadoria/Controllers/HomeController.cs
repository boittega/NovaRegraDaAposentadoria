using NovaRegradaAposentadoria.Models;
using System.IO;
using System.Web.Mvc;

namespace NovaRegradaAposentadoria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ResultView(Resultado model)
        {
            return PartialView(model);
        }

        public ActionResult Calculo(Calculadora CalcForm)
        {
            return PartialView(CalcForm);
        }

        public ActionResult Calcular(Calculadora CalcForm) //[Bind(Include="Idade,TipoTempo,Tempo,Professor")] 
        {
            Resultado mResult = new Resultado();

            int pontosBase = 0, tempoTrabalhado = (CalcForm.TipoTempo == 0 ? CalcForm.Tempo : CalcForm.Idade - CalcForm.Tempo);
            int pontosOp1, pontosOp2, anoBase = System.DateTime.Now.Year + 1;
            int tempoMinimo;
            if (CalcForm.Sexo == "Homem")
            {
                pontosOp1 = pontosOp2 = pontosBase = CalcForm.Professor ? 90 : 95;
                tempoMinimo = 35;
            }
            else
            {
                pontosOp1 = pontosOp2 = pontosBase = CalcForm.Professor ? 80 : 85;
                tempoMinimo = 30;
            }
            pontosOp1 -= CalcForm.Idade + tempoTrabalhado;
            pontosOp2 -= CalcForm.Idade + tempoTrabalhado;
            mResult.IdadeOp1 = mResult.IdadeOp2 = CalcForm.Idade;
            mResult.TempoOp1 = mResult.TempoOp2 = tempoTrabalhado;
            do
            {
                if (anoBase == 2017) { if (pontosOp1 > 0) pontosOp1 += 1; if (pontosOp2 > 0) pontosOp2 += 1; }
                else if (anoBase == 2019) { if (pontosOp1 > 0) pontosOp1 += 1; if (pontosOp2 > 0) pontosOp2 += 1; }
                else if (anoBase == 2020) { if (pontosOp1 > 0) pontosOp1 += 1; if (pontosOp2 > 0) pontosOp2 += 1; }
                else if (anoBase == 2021) { if (pontosOp1 > 0) pontosOp1 += 1; if (pontosOp2 > 0) pontosOp2 += 1; }
                else if (anoBase == 2022) { if (pontosOp1 > 0) pontosOp1 += 1; if (pontosOp2 > 0) pontosOp2 += 1; }

                if (pontosOp1 > 0 || mResult.TempoOp1 < tempoMinimo)
                {
                    if (mResult.TempoOp1 < tempoMinimo)
                    {
                        mResult.TempoOp1++;
                        mResult.IdadeOp1++;
                        pontosOp1 -= 2;
                    }
                    else
                    {
                        mResult.IdadeOp1++;
                        pontosOp1--;
                    }
                }
                if (pontosOp2 > 0 || mResult.TempoOp2 < tempoMinimo)
                {
                    if ((mResult.TempoOp2 < tempoMinimo) || (pontosOp2 > 1))
                    {
                        mResult.TempoOp2++;
                        mResult.IdadeOp2++;
                        pontosOp2 -= 2;
                    }
                    else
                    {
                        mResult.IdadeOp2++;
                        pontosOp2--;
                    }
                }

                anoBase++;
            } while (pontosOp1 > 0 || pontosOp2 > 0 || mResult.TempoOp1 < tempoMinimo || mResult.TempoOp2 < tempoMinimo);

            mResult.IdadeOp1 -= CalcForm.Idade;
            mResult.IdadeOp2 -= CalcForm.Idade;
            mResult.TempoOp1 -= tempoTrabalhado;
            mResult.TempoOp2 -= tempoTrabalhado;

            using (StringWriter sw = new StringWriter())
            {
                var vd = new ViewDataDictionary() { Model = mResult };

                var viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, "ResultView");
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, vd, this.TempData, sw);

                viewResult.View.Render(viewContext, sw);

                return Content(sw.ToString());
            }
        }

        
        public ActionResult AbreCalculo(string Sexo)
        {
            Calculadora CalcForm = new Calculadora();

            CalcForm.Sexo = Sexo == "H" ? "Homem" : "Mulher";

            using (StringWriter sw = new StringWriter())
            {
                var vd = new ViewDataDictionary() { Model = CalcForm };

                var viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, "Calculo");
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, vd, this.TempData, sw);

                viewResult.View.Render(viewContext, sw);

                return Content(sw.ToString());
            }
        }
    }
}