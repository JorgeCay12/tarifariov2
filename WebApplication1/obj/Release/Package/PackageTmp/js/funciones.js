


var index = 0
function addPanel(titulo,url){
	index++;
	var _content = '<div id="form_contenedor' + index +'" class="easyui-panel"  fit="true" border="false"></div>';
	$('#tabs_contenedor').tabs('add',{
		title: titulo.substring(0,10),
		content: _content,
		iconCls: 'icon-form',
		fit:true,
		closable: true,
	});
	$("#form_contenedor" + index).load(url + '?index=' + index, function(){
        $.parser.parse("#form_contenedor" + index);
    });  
}

function createFrame(url) {
	var s = '';
	$.get(url, function(data){
			s=data;
    });
	
	return s;
}

function saveRol(){
// Send the data using post
  var posting = $.post( 'addRol.asp', { id: $("#rol_fcodigo").val(), nombre: $("#rol_fdescripcion").val() } );
 
  // Put the results in a div
  posting.done(function( data ) {
	var nodes = $('#tt').tree('getChecked');
        for(var i=0; i<nodes.length; i++){
            if(100< nodes[i].id){
				  var posting = $.post( 'addRolDet.asp', { id: $("#rol_fcodigo").val(), id2: nodes[i].id } );
            } 
        }
	
    $('#dd').dialog('close');
	$('#gridRol').datagrid('reload'); 
  });
}

function editRol_db(){
// Send the data using post
  var posting = $.post( 'editRol_db.asp', { id: $("#rol_fcodigo").val(), nombre: $("#rol_fdescripcion").val(), estado: $("#cmbestado").combobox('getValue') } );
 
  // Put the results in a div
  posting.done(function( data ) {
	var nodes = $('#tt').tree('getChecked');
        for(var i=0; i<nodes.length; i++){
            if(100< nodes[i].id){
				  var posting = $.post( 'addRolDet.asp', { id: $("#rol_fcodigo").val(), id2: nodes[i].id } );
            } 
        }
	
    $('#dd').dialog('close');
	$('#gridRol').datagrid('reload'); 
  });
}


function borrar_rol(vid){
	 $.messager.confirm('Confirmar','Esta Seguro de Inactivar Rol?',function(r){
		if (r){
			var posting = $.post( 'deleteRol.asp', { id: vid } );
		 
		  // Put the results in a div
			posting.done(function( data ) {
				$('#gridRol').datagrid('reload'); 
			});
		}
	});
}

function editRol(vid){
	$('#dd').dialog({
		title: 'Editar Rol',
		width: 500,
		height: 366,
		closed: false,
		cache: false,
		href: 'editRol.asp?id=' + vid,
		modal: true
	});
}

function borrar_rol_All(){
    var ss = [];
    var rows = $('#gridRol').datagrid('getSelections');
    if (rows){
        $.messager.confirm('Confirmar','Esta Seguro de Inactivar Roles?',function(r){
            if (r){
                for(var i=0; i<rows.length; i++){
                    var row = rows[i];

						var posting = $.post( 'deleteRol.asp', { id: row.COD_GRUPO } );

						posting.done(function( data ) {
						});
                    
                }
                $('#gridRol').datagrid('reload'); 
            }
        });
    } 
}

function borrar_Usuario(vid){
	 $.messager.confirm('Confirmar','Esta Seguro de Inactivar Usuario?',function(r){
		if (r){
			var posting = $.post( 'deleteUsuario.asp', { id: vid } );
		 
		  // Put the results in a div
			posting.done(function( data ) {
				$('#gridUsuario').datagrid('reload'); 
			});
		}
	});
}

function borrar_usuario_All(){
    var ss = [];
    var rows = $('#gridUsuario').datagrid('getSelections');
    if (rows){
        $.messager.confirm('Confirmar','Esta Seguro de Inactivar los Usuarios?',function(r){
            if (r){
                for(var i=0; i<rows.length; i++){
                    var row = rows[i];

						var posting = $.post( 'deleteUsuario.asp', { id: row.cnumdoc_usuario } );

						posting.done(function( data ) {
						});
                    
                }
                $('#gridUsuario').datagrid('reload'); 
            }
        });
    } 
}


function grabarAll( nIndex){

	//return $('#frm_gen').form('enableValidation').form('validate');

	//alert("a")
  
	/*
	$.messager.confirm({
		title: 'My Title',
		msg: 'Are you confirm this?',
		fn: function(r){
			if (r){
				alert('confirmed: '+r);
			} else {
				alert('confirmed: '+r);
			}
		}
	});
  
	return
	*/
  
  $.messager.confirm('Confirmar','Esta Seguro de Grabar los datos?',function(r){
		if (r){
	
	$('body').loader('show');
	
	
	 // Send the data using post
  var posting = $.post( 'saveCV.asp', { DSC_PERFIL: $("#cvperfil").val(), NOM_CV: $("#cvnombre").val(), APE_PAT: $("#cvapepat").val(), APE_MAT: $("#cvapemat").val(), COD_SEX: $("#cmbegenero").combobox('getValue')
  , DES_PAIS: $("#cbocvpais").val(), FEC_NAC: $('#cvfecnac').datebox('getValue'), EDAD: $("#cvedad").val(), TIPO_DOC: $("#cmbtipodoc").combobox('getValue'), NUM_DOC: $("#cvnumdoc").val() } );
	
  // Put the results in a div
  posting.done(function( data ) {
  });
  
  $("#subcont21").find('table').each(function () {
	  var tipo="";
	  var unive="";
	  var pais="";
	  var grado="";
	  var anio="";
	  var vuelta=1;
	 $(this).find('tr').each(function () {
		if(vuelta==1){
			tipo = $(this).find("td").eq(0).find(".cbotipo").html();
		}
		if(vuelta==2){
			unive = $(this).find("td").eq(1).find(".txtuni").html();
		}
		if(vuelta==3){
			pais = $(this).find("td").eq(1).find(".cbopais").html();
		}
		if(vuelta==4){
			grado = $(this).find("td").eq(1).find(".txtgrado").html();
		}
		if(vuelta==5){
			anio = $(this).find("td").eq(1).find(".txtanio").html();
		}
		vuelta++;
	 });
	 
	   var posting = $.post( 'save21.asp', { COD_TIPE: tipo, DSC_UNI: unive, 
	   COD_PAIS: pais, DSC_GRADO: grado, ANIO: anio} );
		
	  // Put the results in a div
	  posting.done(function( data ) {
	  });
  });
  
  
  $("#subcont22").find('table').each(function () {
	  var diplo="";
	  var unive="";
	  var pais="";
	  var anio="";
	  var vuelta=1;
	 $(this).find('tr').each(function () {
		if(vuelta==2){
			diplo = $(this).find("td").eq(1).find(".txtdiplo").html();
		}
		if(vuelta==3){
			unive = $(this).find("td").eq(1).find(".txtuni").html();
		}
		if(vuelta==4){
			pais = $(this).find("td").eq(1).find(".cbopais").html();
		}
		if(vuelta==5){
			anio = $(this).find("td").eq(1).find(".txtanio").html();
		}
		vuelta++;
	 });
	 
	   var posting = $.post( 'save22.asp', { COD_DIPLO: diplo, DSC_UNI: unive, 
	   COD_PAIS: pais, ANIO: anio} );
		
	  // Put the results in a div
	  posting.done(function( data ) {
	  });
  });
  
   $("#subcont23").find('table').each(function () {
	  var progra="";
	  var unive="";
	  var seme="";
	  var vuelta=1;
	 $(this).find('tr').each(function () {
		if(vuelta==2){
			progra = $(this).find("td").eq(1).find(".txtpro").html();
		}
		if(vuelta==3){
			unive = $(this).find("td").eq(1).find(".txtuni").html();
		}
		if(vuelta==4){
			seme = $(this).find("td").eq(1).find(".txtseme").html();
		}
		vuelta++;
	 });
	 
	   var posting = $.post( 'save23.asp', { DSC_PROG: progra, DSC_UNI: unive, 
	   DSC_SEME: seme} );
		
	  // Put the results in a div
	  posting.done(function( data ) {
	  });
  });
  
  
  $("#subcont31").find('table').each(function () {
	  var descr="";

	 $(this).find('tr').each(function () {
		descr = $(this).find("td").eq(1).html();
		
		var posting = $.post( 'save3.asp', { COD_TIPO:'31', DSC_DATO: descr } );
			
		  // Put the results in a div
		 posting.done(function( data ) {
		  });
	 });
  });
  
  $("#subcont32").find('table').each(function () {
	  var descr="";

	 $(this).find('tr').each(function () {
		descr = $(this).find("td").eq(1).html();
		
		var posting = $.post( 'save3.asp', { COD_TIPO:'32', DSC_DATO: descr } );
			
		  // Put the results in a div
		 posting.done(function( data ) {
		  });
	 });
  });
  
  $("#subcont33").find('table').each(function () {
	  var descr="";

	 $(this).find('tr').each(function () {
		descr = $(this).find("td").eq(1).html();
		
		var posting = $.post( 'save3.asp', { COD_TIPO:'33', DSC_DATO: descr } );
			
		  // Put the results in a div
		 posting.done(function( data ) {
		  });
	 });
  });
    $("#subcont34").find('table').each(function () {
	  var descr="";

	 $(this).find('tr').each(function () {
		descr = $(this).find("td").eq(1).html();
		
		var posting = $.post( 'save3.asp', { COD_TIPO:'34', DSC_DATO: descr } );
			
		 // Put the results in a div
		 posting.done(function( data ) {
		  });
	 });
  });
  
   $("#subcont41").find('.formatodetalle').each(function () {
	  var orga="";
	  var pues="";
	  var pais="";
	  var fecha="";
	  var logros="";
	  var vuelta=2;
	 $(this).find('.trdato1').each(function () {
		if(vuelta==2){
			orga = $(this).find("td").eq(1).find(".txtorga").html();
		}
		if(vuelta==3){
			pues = $(this).find("td").eq(1).find(".txtpuesto").html();
		}
		if(vuelta==4){
			pais = $(this).find("td").eq(1).find(".txtpais").html();
		}
		if(vuelta==5){
			fecha = $(this).find("td").eq(1).find(".txtfecha").html();
		}
		if(vuelta==6){
			$(this).find("td").eq(1).find("table").each(function () {
				 $(this).find('tr').each(function () {
					logros=logros + "<tr><td>" + $(this).find("td").eq(0).html() + "</td><td>" +  $(this).find("td").eq(1).html() + "</td></tr>";
				 });
			});
		}
		vuelta++;
	 });
	 
	   var posting = $.post( 'save41.asp', { DSC_ORGA: orga, DSC_PUES: pues, 
	   DSC_PAIS: pais, DSC_FECH: fecha, DSC_LOGROS: logros} );
		
	  // Put the results in a div
	  posting.done(function( data ) {
	  });
  });
  
     $("#subcont42").find('.formatodetalle').each(function () {
	  var proy="";
	  var objetivo="";
	  var alcance="";
	  var puesto="";
	  var pais="";
	  var fecha="";
	  var logros="";
	  var vuelta=2;
	 $(this).find('.trdato1').each(function () {
		if(vuelta==2){
			proy = $(this).find("td").eq(1).find(".txtproy").html();
		}
		if(vuelta==3){
			objetivo = $(this).find("td").eq(1).find(".txtobjetivo").html();
		}
		if(vuelta==4){
			alcance = $(this).find("td").eq(1).find(".txtalcance").html();
		}
		if(vuelta==5){
			puesto = $(this).find("td").eq(1).find(".txtpuesto").html();
		}
		if(vuelta==6){
			pais = $(this).find("td").eq(1).find(".txtpais").html();
		}
		if(vuelta==7){
			fecha = $(this).find("td").eq(1).find(".txtfecha").html();
		}
		if(vuelta==8){
			$(this).find("td").eq(1).find("table").each(function () {
				 $(this).find('tr').each(function () {
					logros=logros + "<tr><td>" + $(this).find("td").eq(0).html() + "</td><td>" +  $(this).find("td").eq(1).html() + "</td></tr>";
				 });
			});
		}
		vuelta++;
	 });
	 
	   var posting = $.post( 'save42.asp', { NOM_PROY: proy, OBJ_PROY: objetivo, 
	   ALC_PROY: alcance, PUES_PROY: puesto, CIU_PROY: pais, FEC_PROY: fecha, LOGROS: logros} );
		
	  // Put the results in a div
	  posting.done(function( data ) {
	  });
  });
  
     $("#subcont43").find('.formatodetalle').each(function () {
	  var orga="";
	  var pues="";
	  var pais="";
	  var logros="";
	  var vuelta=2;
	 $(this).find('.trdato1').each(function () {
		if(vuelta==2){
			orga = $(this).find("td").eq(1).find(".txtorga").html();
		}
		if(vuelta==3){
			pues = $(this).find("td").eq(1).find(".txtpuesto").html();
		}
		if(vuelta==4){
			pais = $(this).find("td").eq(1).find(".txtpais").html();
		}
		if(vuelta==5){
			$(this).find("td").eq(1).find("table").each(function () {
				 $(this).find('tr').each(function () {
					logros=logros + "<tr><td>" + $(this).find("td").eq(0).html() + "</td><td>" +  $(this).find("td").eq(1).html() + "</td></tr>";
				 });
			});
		}
		vuelta++;
	 });
	 
	   var posting = $.post( 'save43.asp', { ORGA_CAPA: orga, PUBL_CAPA: pues, 
	   ANIO_CAPA: pais, TEMA_CAPA: logros} );
		
	  // Put the results in a div
	  posting.done(function( data ) {
	  });
  });
  
  
  
  $("#subcont51").find('table').each(function () {
	  var descr="";

	 $(this).find('tr').each(function () {
		descr = $(this).find("td").eq(1).html();
		
		var posting = $.post( 'save5.asp', { COD_TIPO:'51', DSC_DATO: descr } );
			
		 // Put the results in a div
		 posting.done(function( data ) {
		  });
	 });
  });
    $("#subcont52").find('table').each(function () {
	  var descr="";

	 $(this).find('tr').each(function () {
		descr = $(this).find("td").eq(1).html();
		
		var posting = $.post( 'save5.asp', { COD_TIPO:'52', DSC_DATO: descr } );
			
		 // Put the results in a div
		 posting.done(function( data ) {
		  });
	 });
  });
    $("#subcont53").find('table').each(function () {
	  var descr="";

	 $(this).find('tr').each(function () {
		descr = $(this).find("td").eq(1).html();
		
		var posting = $.post( 'save5.asp', { COD_TIPO:'53', DSC_DATO: descr } );
			
		 // Put the results in a div
		 posting.done(function( data ) {
		  });
	 });
  });
  
   $("#subcont61").find('table').each(function () {
	  var beca="";
	  var ins="";
	  var anio="";
	  var vuelta=1;
	 $(this).find('tr').each(function () {
		if(vuelta==2){
			beca = $(this).find("td").eq(1).find(".txtbeca").html();
		}
		if(vuelta==3){
			ins = $(this).find("td").eq(1).find(".txtins").html();
		}
		if(vuelta==4){
			anio = $(this).find("td").eq(1).find(".txtanio").html();
		}
		vuelta++;
	 });
	 
	   var posting = $.post( 'save6.asp', { DSC_BECA: beca, DSC_INS: ins, 
	   DSC_ANIO: anio} );
		
	  // Put the results in a div
	  posting.done(function( data ) {
	  });
  });
  
  $("#subcont71").find('table').each(function () {
	  var descr="";

	 $(this).find('tr').each(function () {
		descr = $(this).find("td").eq(1).html();
		
		var posting = $.post( 'save7.asp', { DSC_DATO: descr } );
			
		 // Put the results in a div
		 posting.done(function( data ) {
		  });
	 });
  });
  
 
  
  $.messager.alert('Registro','Los datos se grabaron satisfactoriamente.','info');
	$("#form_contenedor" + nIndex).load('cvitae.asp', function(){
        $.parser.parse("#form_contenedor" + nIndex);
    });  
	 $('body').loader('hide');
  	} else {
			
				//alert("no")
				
		
	}
	});
  
}

function agregarRol(){
	$('#dd').dialog({
		title: 'Registro de Rol',
		width: 500,
		height: 336,
		closed: false,
		cache: false,
		href: 'formRol.asp',
		modal: true
	});
}


function agregarUsuario(){
	$('#dd').dialog({
		title: 'Registro de Usuario',
		width: 600,
		height: 436,
		closed: false,
		cache: false,
		href: 'formUsuario.asp',
		modal: true
	});
}




var fecha;
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
function abrirGrilla(titulo, pagina,ancho,alto, variables){
	fecha=$('#cvfecnac').datebox('getValue');
	$('#mF').dialog({
		title: titulo,
		width: ancho,
		height: alto,
		closed: false,
		cache: false,
		href: pagina + '.asp' + variables,
		modal: true
	});
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////
function cerrarSesion(title, url){
	 var posting = $.post('cerrar.asp');
		
	  // Put the results in a div
	  posting.done(function( data ) {
		  window.location.href = "login.asp";
	  });
}



function addTab(title, url){
	if ($('#tabs').tabs('exists', title)){
		$('#tabs').tabs('select', title);
		var currTab = $('#tabs').tabs('getSelected');
		var url = $(currTab.panel('options').content).attr('src');
		if(url != undefined && currTab.panel('options').title != 'Home') {
			$('#tabs').tabs('update',{
				tab:currTab,
				options:{
					content:createFrame(url)
				}
			})
		}
	} else {
		var content = createFrame(url);
		$('#tabs').tabs('add',{
			title:title,
			content:content,
			closable:true
		});
	}
	tabClose();
}

$('.cs-navi-tab').dblclick(function() {
		var $this = $(this);
		var href = $this.attr('src');
		var title = $this.text();
		addTab(title, href);
});

function convert(rows){
			function exists(rows, parentId){
				for(var i=0; i<rows.length; i++){
					if (rows[i].id == parentId) return true;
				}
				return false;
			}
			
			var nodes = [];
			// get the top level nodes
			for(var i=0; i<rows.length; i++){
				var row = rows[i];
				if (!exists(rows, row.parentId)){
					nodes.push({
						id:row.id,
						text:row.name
					});
				}
			}
			
			var toDo = [];
			for(var i=0; i<nodes.length; i++){
				toDo.push(nodes[i]);
			}
			while(toDo.length){
				var node = toDo.shift();	// the parent node
				// get the children nodes
				for(var i=0; i<rows.length; i++){
					var row = rows[i];
					if (row.parentId == node.id){
						var child = {id:row.id,text:row.name};
						if (node.children){
							node.children.push(child);
						} else {
							node.children = [child];
						}
						toDo.push(child);
					}
				}
			}
			return nodes;
		}
		
function abrirReporte(variables){		
	$('#wr').window({
		width:600,
		height:400,
		modal:true,
		title:'Resultado Busqueda',
		href: 'resReporte.asp' + variables
	});
}

function convert2(rows){
			function exists(rows, parentId){
				for(var i=0; i<rows.length; i++){
					if (rows[i].id == parentId) return true;
				}
				return false;
			}
			
			var nodes = [];
			// get the top level nodes
			for(var i=0; i<rows.length; i++){
				var row = rows[i];
				if (!exists(rows, row.parentId)){
					nodes.push({
						id:row.id,
						text:row.name
					});
				}
			}
			
			var toDo = [];
			for(var i=0; i<nodes.length; i++){
				toDo.push(nodes[i]);
			}
			while(toDo.length){
				var node = toDo.shift();	// the parent node
				// get the children nodes
				for(var i=0; i<rows.length; i++){
					var row = rows[i];
					if (row.parentId == node.id){
						var vcheked=false;
						if(row.checked=="true"){
							vcheked=true;
						}
						var child = {id:row.id,text:row.name,checked:vcheked};
						if (node.children){
							node.children.push(child);
						} else {
							node.children = [child];
						}
						toDo.push(child);
					}
				}
			}
			return nodes;
		}
		