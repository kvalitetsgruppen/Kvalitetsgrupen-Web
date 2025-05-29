'use strict';

$(function () {
  var theme = $('html').hasClass('light-style') ? 'default' : 'default-dark',
    basicTree = $('#jstree-basic'),
    customIconsTree = $('#jstree-custom-icons'),
    contextMenu = $('#jstree-context-menu'),
    dragDrop = $('#jstree-drag-drop'),
    checkboxTree = $('#jstree-checkbox'),
    ajaxTree = $('#jstree-ajax');

  // Drag Drop
  // --------------------------------------------------------------------
  if (dragDrop.length) {
    dragDrop.jstree({
      core: {
        themes: {
          name: theme
        },
        check_callback: true,
        data: [
          {
            state: {
              opened: true
            },
            text: 'Scope',
            children: [
              {
                text: 'Introduction.txt',
                type: 'txt'
              },
              {
                text: 'Scope of The Manual.txt',
                type: 'txt'
              },
              {
                text: 'Definitions and Abbreviations.txt',
                type: 'txt'
              },
              {
                text: 'Application Standards and Regulations.txt',
                type: 'txt'
              }
            ]
          },
          {
            state: {
              opened: true
            },
            text: 'Leadership',

            children: [
              {
                text: 'Quality and Environmental Policy.txt',
                type: 'txt'
              },
              {
                text: 'Management Commitment',
                type: 'txt'
              },
              {
                text: 'Roles and Responsibilities.txt',
                type: 'txt'
              },
              {
                text: 'Leadership and Communication.txt',
                type: 'txt'
              },
              {
                text: 'Strategic Planning.txt',
                type: 'txt'
              }
            ]
          },
          {
            text: 'Organization and Competence',
            state: {
              opened: true
            },
            children: [
              {
                text: 'Organizational Structure.txt',
                type: 'txt'
              },
              {
                text: 'Competence, Training, and Awareness.txt',
                type: 'txt'
              },
              {
                text: 'Employee Involvement.txt',
                type: 'txt'
              },
              {
                text: 'Resource Management.txt',
                type: 'txt'
              },
              {
                text: 'Internal Communication.txt',
                type: 'txt'
              }
            ]
          },
          {
            text: 'Processes',
            state: {
              opened: true
            },
            children: [
              {
                text: 'Process Approach.txt',
                type: 'txt'
              },
              {
                text: 'Product Realization.txt',
                type: 'txt'
              },
              {
                text: 'Supplier Management.txt',
                type: 'txt'
              },
              {
                text: 'Customer Satisfaction.txt',
                type: 'txt'
              },
              {
                text: 'Document Control.txt',
                type: 'txt'
              }
            ]
          },
          {
            text: 'Improvement Methods',
            state: {
              opened: true
            },
            children: [
              {
                text: 'Continuous Improvement.txt',
                type: 'txt'
              },
              {
                text: 'Corrective and Preventive Actions.txt',
                type: 'txt'
              },
              {
                text: 'Risk Management.txt',
                type: 'txt'
              },
              {
                text: 'Internal Audits.txt',
                type: 'txt'
              },
              {
                text: 'Non- conformity Management.txt',
                type: 'txt'
              }
            ]
          },
          {
            text: 'Monitoring and Review',
            state: {
              opened: true
            },
            children: [
              {
                text: 'Performance Measurement and Monitoring.txt',
                type: 'txt'
              },
              {
                text: 'Management Review.txt',
                type: 'txt'
              },
              {
                text: 'Environmental Aspects and Impacts.txt',
                type: 'txt'
              },
              {
                text: 'Legal and Other Requirements.txt',
                type: 'txt'
              },
              {
                text: 'Reporting and Documentation.txt',
                type: 'txt'
              }
            ]
          }
        ]
      },
      plugins: ['types', 'dnd', 'contextmenu'],
      types: {
        default: {
          icon: 'bx bx-folder'
        },
        html: {
          icon: 'bx bxl-html5 text-danger'
        },
        css: {
          icon: 'bx bxl-css3 text-info'
        },
        img: {
          icon: 'bx bx-image text-success'
        },
        js: {
          icon: 'bx bxl-nodejs text-warning'
        },
        txt: {
          icon: 'bx bx-file text-secondary' // Icon for TXT files
        }
      }
    });
    dragDrop.on('select_node.jstree', function (e, data) {
      var selectedNodeText = data.node.text;
      console.log('Selected node name:', selectedNodeText); // For debugging
      $('#selected-node-name').text(selectedNodeText); // Display in UI
    });

    dragDrop.on('ready.jstree', function () {
      var treeData = dragDrop.jstree(true).get_json('#', { flat: false });
      var nodeNames = [];

      function collectNodeNames(nodes) {
        nodes.forEach(function (node) {
          nodeNames.push(node.text);
          if (node.children && node.children.length > 0) {
            collectNodeNames(node.children);
          }
        });
      }

      collectNodeNames(treeData);
      console.log('All node names:', nodeNames); // You can replace this with your own logic
    });
  }
});
